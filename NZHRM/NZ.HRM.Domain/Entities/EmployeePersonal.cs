namespace NZ.HRM.Domain.Entities
{
    public class EmployeePersonal
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FatherName { get; set; } = string.Empty;
        public DateOnly DOB { get; set; }
        public string NID { get; set; } = string.Empty;

        public EmployeeMaster? Employee { get; set; }
    }
}
