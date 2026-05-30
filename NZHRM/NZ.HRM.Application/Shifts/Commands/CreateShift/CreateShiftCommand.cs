using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.Shifts.Commands.CreateShift;

public class CreateShiftCommand
{
    [Required(ErrorMessage = "Shift name is required")]
    [MaxLength(100, ErrorMessage = "Shift name must not exceed 100 characters")]
    public string ShiftName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Start time is required")]
    public TimeOnly StartTime { get; set; }

    [Required(ErrorMessage = "End time is required")]
    public TimeOnly EndTime { get; set; }

    public int SortOrder { get; set; } = 1;
}
