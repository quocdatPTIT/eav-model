using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using DynamicField.TicketModel;

namespace DynamicField.EAV
{
    public class EavAttribute : FullAuditedEntity<int>, IMayHaveTenant
    {
        public virtual int? TenantId { get; set; }
        
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public virtual string AttributeCode { get; set; }
        
        [MaxLength(20)]
        [Column(TypeName = "varchar(20)")]
        public virtual string BackendType { get; set; }
        
        [MaxLength(100)]
        public virtual string FrontEndLabel { get; set; }
        
        [ForeignKey("EavEntityType")]
        public virtual int EavEntityTypeId { get; set; }
        
        [NotMapped]
        public virtual EavEntityType EavEntityType { get; set; }
        
        [NotMapped]
        public virtual ICollection<TicketDateTime> TicketDateTimeValues { get; set; }
        
        [NotMapped]
        public virtual ICollection<TicketDecimal> TicketDecimalValues { get; set; }
        
        [NotMapped]
        public virtual ICollection<TicketInt> TicketIntValues { get; set; }
        
        [NotMapped]
        public virtual ICollection<TicketText> TicketTextValues { get; set; }
        
        [NotMapped]
        public virtual ICollection<TicketVarchar> TicketVarcharValues { get; set; }
        
        [NotMapped]
        public virtual ICollection<EavAttributeOption> EavAttributeOptions { get; set; }
    }
}