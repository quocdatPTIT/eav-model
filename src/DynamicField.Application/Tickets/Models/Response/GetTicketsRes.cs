using System;
using System.Collections.Generic;
using Abp.AutoMapper;
using DynamicField.EAV;
using DynamicField.TicketModel;
using DynamicField.Tickets.Interfaces;

namespace DynamicField.Tickets.Models.Response
{
    public class GetTicketsRes
    {
        public int Total { get; set; }
        public IEnumerable<TicketsRes> Tickets { get; set; }
    }
    
    public class TicketsRes
    {
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public DateTime CreationTime { get; set; }
        public IEnumerable<ITicketValue<DateTime>> TicketDateTimeValues { get; set; }
        public IEnumerable<ITicketValue<string>> TicketVarcharValues { get; set; }
        public IEnumerable<ITicketValue<string>> TicketTextValues { get; set; }
        public IEnumerable<ITicketValue<decimal>> TicketDecimalValues { get; set; }
        public IEnumerable<ITicketValue<int>> TicketIntValues { get; set; }
    }
}