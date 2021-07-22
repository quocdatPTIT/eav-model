using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Uow;
using Abp.ObjectMapping;
using Dapper;
using DynamicField.Common;
using DynamicField.EntityFrameworkCore;
using DynamicField.TicketModel;
using DynamicField.Tickets.Interfaces;
using DynamicField.Tickets.Models.Request;
using DynamicField.Tickets.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DynamicField.Tickets
{
    public class TicketAppService : DynamicFieldAppServiceBase, ITicketAppService
    {
        private readonly string _connectionString;
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<TicketDateTime> _ticketDateTimeRepository;
        private readonly IRepository<TicketInt> _ticketIntRepository;
        private readonly IRepository<TicketVarchar> _ticketVarcharRepository;

        public TicketAppService(
            IConfiguration configuration,
            IObjectMapper objectMapper,
            IRepository<Ticket> ticketRepository,
            IRepository<TicketDateTime> ticketDateTimeRepository,
            IRepository<TicketInt> ticketIntRepository,
            IRepository<TicketVarchar> ticketVarcharRepository)
        {
            _objectMapper = objectMapper;
            _ticketRepository = ticketRepository;
            _ticketDateTimeRepository = ticketDateTimeRepository;
            _ticketIntRepository = ticketIntRepository;
            _ticketVarcharRepository = ticketVarcharRepository;
            _connectionString = configuration.GetValue<string>("ConnectionStrings:Default");
        }

        public async Task<bool> CreateTicket(CreateTicketReq req)
        {
            var newTicket = _objectMapper.Map<Ticket>(req);
            var ticketId = await _ticketRepository.InsertAndGetIdAsync(newTicket);

            var dbContext = await CurrentUnitOfWork.GetDbContextAsync<DynamicFieldDbContext>();

            // ----------------------------------------------------
            var ticketDateTimeValues = new List<TicketDateTime>();
            req.AttributeDateTimeValues.ForEach(item =>
            {
                ticketDateTimeValues.Add(new TicketDateTime
                {
                    AttributeId = item.AttributeId,
                    Value = item.Value,
                    TicketId = ticketId
                });
            });

            // ----------------------------------------------------
            var ticketVarcharValues = new List<TicketVarchar>();

            req.AttributeVarcharValues.ForEach(item =>
            {
                ticketVarcharValues.Add(new TicketVarchar
                {
                    AttributeId = item.AttributeId,
                    Value = item.Value,
                    TicketId = ticketId
                });
            });

            // ----------------------------------------------------
            if (ticketDateTimeValues.Count > 0)
                await dbContext.TicketVarcharValues.AddRangeAsync(ticketVarcharValues);

            if (ticketDateTimeValues.Count > 0)
                await dbContext.TicketDateTimeValues.AddRangeAsync(ticketDateTimeValues);
            return await Task.FromResult(true);
        }

        [HttpPost]
        public async Task<GetTicketsRes> GetTickets(GetTicketsReq req)
        {
            GetTicketsRes response = new GetTicketsRes();
            var optionTypes = new List<string> {"DROPDOWN", "MULTISELECT", "CHECKBOX"};
            using (var connection = GetConnection())
            {
                // ---------------------------------------------------
                // Count total ticket
                var countTicketSql = new StringBuilder("");
                if (req.DecimalFilters is null && req.IntFilters is null &&
                    req.TextFilters is null && req.VarcharFilters is null &&
                    req.DateTimeFilters is null)
                {
                    countTicketSql.Append("SELECT t.Id FROM Tickets AS t ORDER BY t.Id DESC");
                }
                else
                {
                    countTicketSql.Append("SELECT T.Id FROM Tickets as T ");
                    if (req.DateTimeFilters is not null)
                    {
                        for (int i = 0; i < req.DateTimeFilters.Count; i++)
                        {
                            var filter = req.DateTimeFilters[i];
                            countTicketSql.Append($"LEFT JOIN TicketDateTimeValues AS TDDV_{i} ON T.Id = TDDV_{i}.TicketId AND TDDV_{i}.AttributeId = {filter.AttributeId} ");
                        }
                    }

                    if (req.DecimalFilters is not null)
                    {
                        for (int i = 0; i < req.DecimalFilters.Count; i++)
                        {
                            var filter = req.DecimalFilters[i];
                            countTicketSql.Append($"LEFT JOIN TicketDecimalValues AS TCV_{i} ON T.Id = TCV_{i}.TicketId AND TCV_{i}.AttributeId = {filter.AttributeId} ");
                        }
                    }
                    
                    if (req.IntFilters is not null)
                    {
                        for (int i = 0; i < req.IntFilters.Count; i++)
                        {
                            var filter = req.IntFilters[i];
                            countTicketSql.Append($"LEFT JOIN TicketIntValues AS TIN_{i} ON T.Id = TIN_{i}.TicketId AND TIN_{i}.AttributeId = {filter.AttributeId} ");
                        }
                    }
                    
                    if (req.VarcharFilters is not null)
                    {
                        for (int i = 0; i < req.VarcharFilters.Count; i++)
                        {
                            var filter = req.VarcharFilters[i];
                            countTicketSql.Append($"LEFT JOIN TicketVarcharValues AS TVV_{i} ON T.Id = TVV_{i}.TicketId AND TVV_{i}.AttributeId = {filter.AttributeId} ");
                        }
                    }
                    
                    if (req.TextFilters is not null)
                    {
                        for (int i = 0; i < req.TextFilters.Count; i++)
                        {
                            var filter = req.TextFilters[i];
                            countTicketSql.Append($"LEFT JOIN TicketTextValues AS TTV_{i} ON T.Id = TTV_{i}.TicketId AND TTV_{i}.AttributeId = {filter.AttributeId} ");
                        }
                    }
                    
                    countTicketSql.Append("WHERE 1 = 1 AND ");
                    if (req.DateTimeFilters is not null)
                    {
                        for (int i = 0; i < req.DateTimeFilters.Count; i++)
                        {
                            var filter = req.DateTimeFilters[i];
                            countTicketSql.Append($"TDDV_{i}.Value > '{filter.Value}' AND ");
                        }
                    }
                    
                    if (req.DecimalFilters is not null)
                    {
                        for (int i = 0; i < req.DecimalFilters.Count; i++)
                        {
                            var filter = req.DecimalFilters[i];
                            countTicketSql.Append($"TCV_{i}.Value = {filter.Value} AND ");
                        }
                    }
                    
                    if (req.IntFilters is not null)
                    {
                        for (int i = 0; i < req.IntFilters.Count; i++)
                        {
                            var filter = req.IntFilters[i];
                            countTicketSql.Append($"TIN_{i}.Value = {filter.Value} AND ");
                        }
                    }
                    
                    if (req.VarcharFilters is not null)
                    {
                        for (int i = 0; i < req.VarcharFilters.Count; i++)
                        {
                            var filter = req.VarcharFilters[i];
                            countTicketSql.Append($"TVV_{i}.Value LIKE N'{filter.Value}' AND ");
                        }
                    }
                    
                    if (req.TextFilters is not null)
                    {
                        for (int i = 0; i < req.TextFilters.Count; i++)
                        {
                            var filter = req.TextFilters[i];
                            countTicketSql.Append($"TTV_{i}.Value LIKE N'{filter.Value}' AND ");
                        }
                    }

                    countTicketSql.Remove(countTicketSql.Length - 4, 4);
                    countTicketSql.Append("ORDER BY T.Id DESC");
                }
                var totalTicketIds = await connection.QueryAsync<int>(countTicketSql.ToString());
                response.Total = totalTicketIds.Count();
                var paginationTicketIds = totalTicketIds.Skip(req.Skip).Take(req.Take).ToList();


                // ---------------------------------------------------
                // Get attributes
                const string getAttributesSql =
                    @"SELECT Id, AttributeCode, BackendType, FrontEndLabel FROM EavAttributes WHERE EavEntityTypeId = 1";

                var attributesFromRepo = await connection.QueryAsync<AttributeTicket>(getAttributesSql);
                var attributeIds = attributesFromRepo.Where(a => optionTypes.Contains(a.BackendType))
                    .Select(a => a.Id).ToList();

                var attributes = attributesFromRepo.ToDictionary(a => a.Id);
                
                // ---------------------------------------------------
                // Get options
                const string optionValuesSql = @"
                        SELECT EAOV.Id, EAOV.Value FROM EavAttributeOptionValues EAOV
                        INNER JOIN EavAttributeOptions EAO ON EAO.Id = EAOV.AttributeOptionId
                        WHERE EAO.AttributeId IN @attributeIds
                    ";

                var optionValues = (await connection.QueryAsync<OptionValueReq>(optionValuesSql, new {attributeIds}))
                    .ToDictionary(v => v.Id);
                
                // ---------------------------------------------------
                // Mapping value
                const string getTicketsSql = @"
                    SELECT Id, TenantId, Title, Status, CreationTime FROM Tickets WHERE Id IN @ticketIds
                    SELECT Id, TicketId, Value, AttributeId FROM TicketDateTimeValues WHERE TicketId IN @ticketIds
                    SELECT Id, TicketId, Value, AttributeId FROM TicketVarcharValues WHERE TicketId IN @ticketIds
                    SELECT Id, TicketId, Value, AttributeId FROM TicketTextValues WHERE TicketId IN @ticketIds
                    SELECT Id, TicketId, Value, AttributeId FROM TicketDecimalValues WHERE TicketId IN @ticketIds
                    SELECT Id, TicketId, Value, AttributeId FROM TicketIntValues WHERE TicketId IN @ticketIds
                ";

                using (var multi =
                    await connection.QueryMultipleAsync(getTicketsSql, new {ticketIds = paginationTicketIds}))
                {
                    // --- Get tickets ---
                    var tickets = await multi.ReadAsync<TicketsRes>();

                    // --- Get datetime value ---
                    var timeValues = await multi.ReadAsync<TicketValue<DateTime>>();
                    var dateTimeDictionary =
                        ConvertToDictionary<ITicketValue<DateTime>, DateTime>(timeValues, attributes, optionValues);
                    MappingValues(ref tickets, dateTimeDictionary, Constants.TicketValueType.DATETIME);

                    // --- Get varchar value ---
                    var varcharValues = await multi.ReadAsync<TicketValue<string>>();
                    var varcharDictionary =
                        ConvertToDictionary<ITicketValue<string>, string>(varcharValues, attributes, optionValues);
                    MappingValues(ref tickets, varcharDictionary, Constants.TicketValueType.VARCHAR);

                    // --- Get text value ---
                    var textValues = await multi.ReadAsync<TicketValue<string>>();
                    var textDictionary = ConvertToDictionary<ITicketValue<string>, string>(textValues, attributes, optionValues);
                    MappingValues(ref tickets, textDictionary, Constants.TicketValueType.TEXT);

                    // --- Get decimal value ---
                    var decimalValues = await multi.ReadAsync<TicketValue<decimal>>();
                    var decimalDictionary =
                        ConvertToDictionary<ITicketValue<decimal>, decimal>(decimalValues, attributes, optionValues);
                    MappingValues(ref tickets, decimalDictionary, Constants.TicketValueType.DECIMAL);

                    // --- Get int value ---
                    var intValues = await multi.ReadAsync<TicketValue<int>>();
                    var intDictionary = ConvertToDictionary<ITicketValue<int>, int>(intValues, attributes, optionValues);
                    MappingValues(ref tickets, intDictionary, Constants.TicketValueType.INT);

                    response.Tickets = tickets;
                }
            }

            return await Task.FromResult(response);
        }

        private void MappingValues<T>(ref IEnumerable<TicketsRes> tickets,
            Dictionary<int, HashSet<ITicketValue<T>>> dictionary, Constants.TicketValueType type)
        {
            foreach (var ticket in tickets)
            {
                if (dictionary.ContainsKey(ticket.Id))
                    switch (type)
                    {
                        case Constants.TicketValueType.INT:
                            ticket.TicketIntValues = (IEnumerable<ITicketValue<int>>) dictionary[ticket.Id];
                            break;
                        case Constants.TicketValueType.DECIMAL:
                            ticket.TicketDecimalValues = (IEnumerable<ITicketValue<decimal>>) dictionary[ticket.Id];
                            break;
                        case Constants.TicketValueType.DATETIME:
                            ticket.TicketDateTimeValues = (IEnumerable<ITicketValue<DateTime>>) dictionary[ticket.Id];
                            break;
                        case Constants.TicketValueType.TEXT:
                            ticket.TicketTextValues = (IEnumerable<ITicketValue<string>>) dictionary[ticket.Id];
                            break;
                        case Constants.TicketValueType.VARCHAR:
                            ticket.TicketVarcharValues = (IEnumerable<ITicketValue<string>>) dictionary[ticket.Id];
                            break;
                    }
            }
        }

        private Dictionary<int, HashSet<T>> ConvertToDictionary<T, U>(IEnumerable<T> values,
            Dictionary<int, AttributeTicket> attrDictionary,
            Dictionary<int, OptionValueReq> optionValueDictionary) where T : ITicketValue<U>
        {
            var dict = new Dictionary<int, HashSet<T>>();
            List<string> singleType = new List<string> {"DROPDOWN", "CHECKBOX"};
            foreach (var value in values)
            {
                if (attrDictionary.ContainsKey(value.AttributeId))
                {
                    value.Attribute = attrDictionary[value.AttributeId];

                    if (optionValueDictionary is not null && singleType.Contains(value.Attribute.BackendType))
                    {
                        int optionValueId = Convert.ToInt32(value.Value.ToString());
                        if (optionValueDictionary.ContainsKey(optionValueId))
                            value.Attribute.SingleSelectValue = optionValueDictionary[optionValueId];
                    }
                    else if (optionValueDictionary is not null && value.Attribute.BackendType == "MULTISELECT")
                    {
                        var optionValueIds = value.Value.ToString().Split(',').ToList();
                        value.Attribute.MultiSelectValue = new List<OptionValueReq>();
                        
                        optionValueIds.ForEach(id =>
                        {
                            var optionValueId = Convert.ToInt32(id);
                            if (optionValueDictionary.ContainsKey(optionValueId))
                                value.Attribute.MultiSelectValue.Add(optionValueDictionary[optionValueId]);
                        });
                    }
                }

                if (dict.ContainsKey(value.TicketId))
                    dict[value.TicketId].Add(value);
                else
                    dict.Add(value.TicketId, new HashSet<T> {value});
            }

            return dict;
        }

        private IDbConnection GetConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();

            return connection;
        }
    }
}