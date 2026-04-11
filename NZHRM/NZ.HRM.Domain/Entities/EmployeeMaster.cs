namespace NZ.HRM.Domain.Entities
{
    public class EmployeeMaster
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public int UnitId { get; set; }
        public int DepartmentId { get; set; }
        public int SectionId { get; set; }
        public int DesignationId { get; set; }
        public int GradeId { get; set; }
        public DateOnly JoiningDate { get; set; }
        public string Status { get; set; } = string.Empty;

        public Company? Company { get; set; }
        public Unit? Unit { get; set; }
        public Department? Department { get; set; }
        public Section? Section { get; set; }
        public Designation? Designation { get; set; }
        public Grade? Grade { get; set; }
    }
}
