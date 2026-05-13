using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.Designations.Commands.UpdateDesignation;

public class UpdateDesignationCommand
{
    public string Id { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string DesignationName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? DesignationCode { get; set; }

    public string? ParentId { get; set; }
}
