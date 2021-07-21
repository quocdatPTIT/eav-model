using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace DynamicField.TicketModel
{
    public class Ticket : FullAuditedEntity<int>, IMayHaveTenant
    {
        public virtual int? TenantId { get; set; }
        
        [MaxLength(255)]
        public virtual string Title { get; set; }
        
        [MaxLength(20)]
        public virtual string Status { get; set; }
        
        public virtual ICollection<TicketDateTime> TicketDateTimeValues { get; set; }
        
        public virtual ICollection<TicketDecimal> TicketDecimalValues { get; set; }
        
        public virtual ICollection<TicketInt> TicketIntValues { get; set; }
        
        public virtual ICollection<TicketText> TicketTextValues { get; set; }
        
        public virtual ICollection<TicketVarchar> TicketVarcharValues { get; set; }
    }
}