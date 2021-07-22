using System;
using System.Collections.Generic;

namespace DynamicField.Tickets.Models.Request
{
    public class GetTicketsReq
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public TicketFilter<DateTime> DateTimeFilters { get; set; }
        public TicketFilter<decimal> DecimalFilters { get; set; }
        public TicketFilter<int> IntFilters { get; set; }
        public TicketFilter<string> VarcharFilters { get; set; }
        public TicketFilter<string> TextFilters { get; set; }
    }

    public class FilterValues<T>
    {
        public string OptionType { get; set; }
        public int AttributeId { get; set; }
        public T Value { get; set; }
    }

    public class TicketFilter<T>
    {
        public string BackendType { get; set; }
        public List<FilterValues<T>> Filters { get; set; }
    }
}