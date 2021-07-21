using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using DynamicField.EAV;

namespace DynamicField.TicketModel
{
    public class TicketDateTime : FullAuditedEntity<int>, IMayHaveTenant
    {
        public virtual int? TenantId { get; set; }
        public virtual DateTime? Value { get; set; }

        [ForeignKey("EavAttribute")]
        public virtual int AttributeId { get; set; }
        public virtual EavAttribute EavAttribute { get; set; }
        
        [ForeignKey("Ticket")]
        public virtual int TicketId { get; set; }
        
        [NotMapped]
        public virtual Ticket Ticket { get; set; }
    }
}