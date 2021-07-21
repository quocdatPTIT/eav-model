using System;
using System.Collections.Generic;

namespace DynamicField.Tickets.Models.Request
{
    public class GetTicketsReq
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public List<FilterValues<DateTime>> DateTimeFilters { get; set; }
        public List<FilterValues<decimal>> DecimalFilters { get; set; }
        public List<FilterValues<int>> IntFilters { get; set; }
        public List<FilterValues<string>> VarcharFilters { get; set; }
        public List<FilterValues<string>> TextFilters { get; set; }
    }

    public class FilterValues<T>
    {
        public string BackendType { get; set; }
        public int AttributeId { get; set; }
        public T Value { get; set; }
    }
}