using NZ.HRM.Domain.Common;
using NZ.HRM.Utility.Enum;
using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Domain.Entities;

public class PhysicalExaminationSetting : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string FieldName { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }

    public PhysicalExaminationFieldType FieldType { get; set; } = PhysicalExaminationFieldType.Binary;

    [MaxLength(2000)]
    public string? OptionValuesJson { get; set; }
}
