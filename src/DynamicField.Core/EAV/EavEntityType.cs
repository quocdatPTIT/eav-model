using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace DynamicField.EAV
{
    public class EavEntityType : FullAuditedEntity<int>, IMayHaveTenant
    {
        public virtual int? TenantId { get; set; }
        
        [MaxLength(20)]
        [Column(TypeName = "varchar(20)")]
        public virtual string EntityTypeCode { get; set; }
        
        [MaxLength(20)]
        [Column(TypeName = "varchar(20")]
        public virtual string EntityTable { get; set; }
        
        public virtual ICollection<EavAttribute> Attributes { get; set; }
    }
}