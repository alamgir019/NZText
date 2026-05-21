using NZ.HRM.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Domain.Entities;

public class PhysicalExaminationSetting : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string FieldName { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }

    public bool IsBinaryCheck { get; set; } = true;

    public bool AllowRemarks { get; set; } = false;
}
