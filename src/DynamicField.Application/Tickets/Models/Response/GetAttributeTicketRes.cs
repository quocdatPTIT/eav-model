using System.Collections.Generic;

namespace DynamicField.Tickets.Models.Response
{
    public class GetAttributeTicketRes
    {
        public IEnumerable<AttributeTicketValue> Attributes { get; set; }
    }
    
    public class AttributeTicketValue
    {
        public int Id { get; set; }
        public string AttributeCode { get; set; }
        public string BackendType { get; set; }
        public string FrontEndLabel { get; set; }
    }
}