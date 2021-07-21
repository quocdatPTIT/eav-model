using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace DynamicField.EAV
{
    public class EavAttributeOption : FullAuditedEntity<int>, IMayHaveTenant
    {
        public virtual int? TenantId { get; set; }

        public virtual int? SortOrder { get; set; }

        [ForeignKey("EavAttribute")]
        public virtual int AttributeId { get; set; }
        
        public virtual EavAttribute EavAttribute { get; set; }

        public virtual ICollection<EavAttributeOptionValue> EavAttributeOptionValues { get; set; }
    }
}