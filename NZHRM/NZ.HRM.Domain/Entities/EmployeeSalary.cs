namespace NZ.HRM.Domain.Entities
{
    public class EmployeeSalary
    {
        public int EmployeeId { get; set; }
        public decimal Basic { get; set; }
        public decimal HouseRent { get; set; }
        public decimal Medical { get; set; }
        public decimal Gross { get; set; }
        public DateOnly EffectiveDate { get; set; }

        public EmployeeMaster? Employee { get; set; }
    }
}
