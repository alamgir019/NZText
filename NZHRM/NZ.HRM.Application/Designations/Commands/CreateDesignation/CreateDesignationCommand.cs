using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.Designations.Commands.CreateDesignation;

public class CreateDesignationCommand
{
    [Required]
    [MaxLength(100)]
    public string DesignationName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? DesignationCode { get; set; }

    public string? ParentId { get; set; }
}
