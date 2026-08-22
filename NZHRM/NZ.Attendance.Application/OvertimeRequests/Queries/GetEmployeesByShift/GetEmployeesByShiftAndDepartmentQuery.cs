namespace NZ.Attendance.Application.OvertimeRequests.Queries.GetEmployeesByShift
{
    public class GetEmployeesByShiftAndDepartmentQuery
    {
        public string ShiftId { get; set; } = string.Empty;
        public string DepartmentId { get; set; } = string.Empty;
    }
}
