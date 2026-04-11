namespace NZ.HRM.Domain.Entities
{
    public class Grade
    {
        public int GradeId { get; set; }
        public string GradeName { get; set; } = string.Empty;
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
    }
}
