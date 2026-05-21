using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.PhysicalExaminationSettings.Commands.CreatePhysicalExaminationSetting;

public class CreatePhysicalExaminationSettingCommand
{
    [Required]
    [MaxLength(100)]
    public string FieldName { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int DisplayOrder { get; set; }

    public bool IsBinaryCheck { get; set; } = true;

    public bool AllowRemarks { get; set; } = false;
}
