using System.Collections.Generic;

namespace DynamicField.Tickets.Interfaces
{
    public interface ITicketValue<T>
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public T Value { get; set; }
        
        public int AttributeId { get; set; }
        
        public AttributeTicket Attribute { get; set; }
    }

    public class TicketValue<T> : ITicketValue<T>
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public T Value { get; set; }
        public int AttributeId { get; set; }
        public AttributeTicket Attribute { get; set; }
    }

    public class AttributeTicket
    {
        public int Id { get; set; }
        public string AttributeCode { get; set; }
        public string BackendType { get; set; }
        public string FrontEndLabel { get; set; }
        public OptionValueReq SingleSelectValue { get; set; }
        public List<OptionValueReq> MultiSelectValue { get; set; }
    }

    public class OptionValueReq
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}