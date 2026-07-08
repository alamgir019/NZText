using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.GroupComplexes.Commands.UpdateGroupComplex;

public class UpdateGroupComplexCommand
{
    [Required]
    public string Id { get; set; } = string.Empty;

    [Required(ErrorMessage = "GroupId is required")]
    public string GroupId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Complex code is required")]
    [MaxLength(50)]
    public string ComplexCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Complex name is required")]
    [MaxLength(200)]
    public string ComplexName { get; set; } = string.Empty;
}
