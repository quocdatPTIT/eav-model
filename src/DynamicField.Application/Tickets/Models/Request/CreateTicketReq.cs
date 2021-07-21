using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using DynamicField.TicketModel;

namespace DynamicField.Tickets.Models.Request
{
    [AutoMapTo(typeof(Ticket))]
    public class CreateTicketReq
    {
        [Required]
        public string Status { get; set; }

        [Required]
        public string Title { get; set; }
        
        public List<AttributeValue<DateTime>> AttributeDateTimeValues { get; set; }
        
        public List<AttributeValue<decimal>> AttributeDecimalValues { get; set; }
        
        public List<AttributeValue<int>> AttributeIntValues { get; set; }
        
        public List<AttributeValue<string>> AttributeTextTimeValues { get; set; }
        
        public List<AttributeValue<string>> AttributeVarcharValues { get; set; }

        public CreateTicketReq()
        {
            AttributeDecimalValues = new List<AttributeValue<decimal>>();
            AttributeDateTimeValues = new List<AttributeValue<DateTime>>();
            AttributeIntValues = new List<AttributeValue<int>>();
            AttributeTextTimeValues = new List<AttributeValue<string>>();
            AttributeVarcharValues = new List<AttributeValue<string>>();
        }
    }

    public class AttributeValue<T>
    {
        [Required]
        public int AttributeId { get; set; }
        
        [Required]
        public T Value { get; set; }
    }
}