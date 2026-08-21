namespace NZ.Attendance.Application.OvertimeRequests.Dto
{
    public class EmployeeByShiftDto
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeCode { get; set; } = string.Empty;
        public string? DesignationId { get; set; }
        public string DesignationName { get; set; } = string.Empty;
        public string? DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
    }
}
