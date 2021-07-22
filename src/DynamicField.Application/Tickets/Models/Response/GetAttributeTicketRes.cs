using System.Collections.Generic;

namespace DynamicField.Tickets.Models.Response
{
    public class GetAttributeTicketRes
    {
        public IEnumerable<AttributeTicketValue> Attributes { get; set; }
    }
    
    public class AttributeTicketValue
    {
        public string Field { get; set; }
        public string Header { get; set; }
    }
}