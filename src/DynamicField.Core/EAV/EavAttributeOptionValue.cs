using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace DynamicField.EAV
{
    public class EavAttributeOptionValue : FullAuditedEntity<int>, IMayHaveTenant
    {
        public virtual int? TenantId { get; set; }

        [MaxLength(255)]
        public virtual string Value { get; set; }

        [ForeignKey("EavAttributeOption")]
        public virtual int AttributeOptionId { get; set; }
        
        public virtual EavAttributeOption EavAttributeOption { get; set; }
    }
}