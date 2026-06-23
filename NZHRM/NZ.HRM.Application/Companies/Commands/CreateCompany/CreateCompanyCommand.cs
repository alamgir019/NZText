using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.Companies.Commands.CreateUnit;

public class CreateUnitCommand
{
    [Required(ErrorMessage = "Unit code is required")]
    [MaxLength(10, ErrorMessage = "Unit code must not exceed 10 characters")]
    public string UnitCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Unit name is required")]
    [MaxLength(100, ErrorMessage = "Unit name must not exceed 100 characters")]
    public string UnitName { get; set; } = string.Empty;

    // Location association is handled via UnitLocation mapping entity

    public bool IsCompliant { get; set; } = false;
}