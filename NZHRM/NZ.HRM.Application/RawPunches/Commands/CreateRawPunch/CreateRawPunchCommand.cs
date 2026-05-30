using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.RawPunches.Commands.CreateRawPunch;

public class CreateRawPunchCommand
{
    [Required]
    public string EmployeeId { get; set; } = string.Empty;

    [Required]
    public DateTime PunchDate { get; set; }

    [Required]
    public TimeSpan PunchTime { get; set; }

    [MaxLength(10)]
    public string? PunchType { get; set; }

    [MaxLength(50)]
    public string? DeviceId { get; set; }

    [MaxLength(50)]
    public string? EmployeeCode { get; set; }
}
