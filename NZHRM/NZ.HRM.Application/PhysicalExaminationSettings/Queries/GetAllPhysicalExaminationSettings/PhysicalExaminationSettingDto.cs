using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.PhysicalExaminationSettings.Queries.GetAllPhysicalExaminationSettings;

public class PhysicalExaminationSettingDto
{
    public string Id { get; set; } = string.Empty;
    public string FieldName { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public PhysicalExaminationFieldType FieldType { get; set; }
    public string? OptionValuesJson { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime UpdatedOn { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
