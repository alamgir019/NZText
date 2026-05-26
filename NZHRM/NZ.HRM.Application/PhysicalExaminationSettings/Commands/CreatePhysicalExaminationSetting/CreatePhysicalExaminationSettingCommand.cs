using System.ComponentModel.DataAnnotations;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.PhysicalExaminationSettings.Commands.CreatePhysicalExaminationSetting;

public class CreatePhysicalExaminationSettingCommand
{
    [Required]
    [MaxLength(100)]
    public string FieldName { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int DisplayOrder { get; set; }

    public PhysicalExaminationFieldType FieldType { get; set; } = PhysicalExaminationFieldType.Binary;

    [MaxLength(2000)]
    public string? OptionValuesJson { get; set; }
}
