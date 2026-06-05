using NZ.HRM.Domain.Common;
using NZ.HRM.Utility.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZ.HRM.Domain.Entities;


[Table("physical_examination_setting", Schema = "hrm")]
public class HrmPhysicalExaminationSetting : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string FieldName { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }

    public PhysicalExaminationFieldType FieldType { get; set; } = PhysicalExaminationFieldType.Binary;

    [MaxLength(2000)]
    public string? OptionValuesJson { get; set; }
}
