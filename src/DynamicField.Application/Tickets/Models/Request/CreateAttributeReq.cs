using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using DynamicField.EAV;
using JetBrains.Annotations;

namespace DynamicField.Tickets.Models.Request
{
    [AutoMapTo(typeof(EavAttribute))]
    public class CreateAttributeReq
    {
        [Required]
        public string AttributeCode { get; set; }
        
        [Required]
        public string BackendType { get; set; }
        
        [Required]
        public string FrontEndLabel { get; set; }
        
        [Required]
        public string EntityTypeCode { get; set; }
        
        [CanBeNull] public Option Option { get; set; }
    }

    public class Option
    {
        [Required]
        public int SortOrder { get; set; }

        [Required]
        public List<string> OptionValues { get; set; }
    }

    public class OptionValue
    {
        [Required]
        public string Value { get; set; }
    }
}