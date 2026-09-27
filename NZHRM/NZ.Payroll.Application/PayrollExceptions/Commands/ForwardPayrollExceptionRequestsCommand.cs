using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NZ.Payroll.Application.PayrollExceptions.Commands;

public class ForwardPayrollExceptionRequestsCommand
{
    [Required]
    public List<string> RequestIds { get; set; } = new();

    [MaxLength(250)]
    public string? Remarks { get; set; }

    [JsonIgnore]
    public string ForwardedBy { get; set; } = string.Empty;
}
