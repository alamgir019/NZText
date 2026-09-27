using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NZ.Payroll.Application.PayrollExceptions.Commands;

public class ForwardPayrollExceptionRequestCommand
{
    [Required]
    public string RequestId { get; set; } = string.Empty;

    [MaxLength(250)]
    public string? Remarks { get; set; }

    [JsonIgnore]
    public string ForwardedBy { get; set; } = string.Empty;
}
