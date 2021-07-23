using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Uow;
using Abp.ObjectMapping;
using Dapper;
using DynamicField.EAV;
using DynamicField.EntityFrameworkCore;
using DynamicField.ExtentionMethods;
using DynamicField.Tickets.Models.Request;
using DynamicField.Tickets.Models.Response;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DynamicField.Tickets
{
    public class EavTicketAppService : DynamicFieldAppServiceBase, IEavTicketAppService
    {
        private readonly string _connectionString;
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<EavAttribute> _eavAttributeRepository;
        private readonly IRepository<EavEntityType> _eavEntityTypeRepository;
        private readonly IRepository<EavAttributeOption> _eavAttributeOptionRepository;

        public EavTicketAppService(
            IConfiguration configuration,
            IObjectMapper objectMapper,
            IRepository<EavAttribute> eavAttributeRepository,
            IRepository<EavEntityType> eavEntityTypeRepository,
            IRepository<EavAttributeOption> eavAttributeOptionRepository)
        {
            _objectMapper = objectMapper;
            _eavAttributeRepository = eavAttributeRepository;
            _eavEntityTypeRepository = eavEntityTypeRepository;
            _eavAttributeOptionRepository = eavAttributeOptionRepository;
            _connectionString = configuration.GetValue<string>("ConnectionStrings:Default");
        }

        public async Task<bool> CreateAttribute(CreateAttributeReq res)
        {
            var optionTypes = new List<string> {"DROPDOWN", "MULTISELECT", "CHECKBOX"};

            // Add attribute
            var entityTypeFromRepo =
                await _eavEntityTypeRepository.FirstOrDefaultAsync(e => e.EntityTypeCode == res.EntityTypeCode);

            if (entityTypeFromRepo is null)
                return await Task.FromResult(false);
        
            
            var newAttribute = _objectMapper.Map<EavAttribute>(res);
            newAttribute.EavEntityTypeId = entityTypeFromRepo.Id;
            newAttribute.EavEntityType = entityTypeFromRepo;
            newAttribute.AttributeCode = newAttribute.FrontEndLabel.RemoveUnicode();

            var attributeId = await _eavAttributeRepository.InsertAndGetIdAsync(newAttribute);

            // Add options
            if (!optionTypes.Contains(res.BackendType)) return await Task.FromResult(true);
            
            var option = new EavAttributeOption {AttributeId = attributeId, SortOrder = res.Option?.SortOrder};
            var optionId = await _eavAttributeOptionRepository.InsertAndGetIdAsync(option);

            var optionValues = new List<EavAttributeOptionValue>();
            
            res.Option?.OptionValues.ForEach(item =>
            {
                optionValues.Add(new EavAttributeOptionValue
                {
                    AttributeOptionId = optionId,
                    Value = item
                });
            });
            
            var dbContext = await CurrentUnitOfWork.GetDbContextAsync<DynamicFieldDbContext>();
            await dbContext.EavAttributeOptionValues.AddRangeAsync(optionValues);
            
            return await Task.FromResult(true);
        }
        
        public async Task<GetAttributeTicketRes> GetAttributesTicket()
        {
            var response = new GetAttributeTicketRes();
            using (var connection = await GetConnection())
            {
                const string sql = @"SELECT AttributeCode as Field, FrontEndLabel as Header
                                    FROM EavAttributes
                                    WHERE EavEntityTypeId = 1
                ";

                var attributesFromRepo = await connection.QueryAsync<AttributeTicketValue>(sql);

                foreach (var item in attributesFromRepo)
                {
                    item.Field = item.Field.LowerFirstLetter();
                }
                
                response.Attributes = attributesFromRepo;
            }
            return await Task.FromResult(response);
        }
        
        private async Task<IDbConnection> GetConnection()
        {
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            return connection;
        }
    }
}