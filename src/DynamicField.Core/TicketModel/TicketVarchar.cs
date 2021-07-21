using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using DynamicField.EAV;

namespace DynamicField.TicketModel
{
    public class TicketVarchar : FullAuditedEntity<int>, IMayHaveTenant
    {
        public virtual int? TenantId { get; set; }
        
        [MaxLength(255)]
        [Column(TypeName = "varchar(255)")]
        public virtual string Value { get; set; }
        
        [ForeignKey("EavAttribute")]
        public virtual int AttributeId { get; set; }
        public virtual EavAttribute EavAttribute { get; set; }
        
        [ForeignKey("Ticket")]
        public virtual int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}