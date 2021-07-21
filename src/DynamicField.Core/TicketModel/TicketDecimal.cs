using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using DynamicField.EAV;

namespace DynamicField.TicketModel
{
    public class TicketDecimal : FullAuditedEntity<int>, IMayHaveTenant
    {
        public virtual int? TenantId { get; set; }
        public virtual Decimal? Value { get; set; }
        
        [ForeignKey("EavAttribute")]
        public virtual int AttributeId { get; set; }
        public virtual EavAttribute EavAttribute { get; set; }
        
        [ForeignKey("Ticket")]
        public virtual int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}