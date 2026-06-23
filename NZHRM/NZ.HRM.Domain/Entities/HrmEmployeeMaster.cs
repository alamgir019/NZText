using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("employee_master", Schema = "hrm")]
    public class HrmEmployeeMaster : BaseEntityWithSortOrder
    {
        // Core identity
        public string EmployeeCode { get; set; } = string.Empty;
        public string EnrollmentId { get; set; } = string.Empty;
        public string CardNo { get; set; } = string.Empty;
        public string? OldCardNo { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeNameBangla { get; set; } = string.Empty;
        public string EmployeeNameEnglish { get; set; } = string.Empty;
        public string EmployeeType { get; set; } = string.Empty; // e.g. "Worker", "Staff", "Officer", "Manager", "Director"
        public string Status { get; set; } = string.Empty;

        // One-to-one related sections
        public HrmEmployeePersonal? Personal { get; set; }
        public HrmEmployeeContact? Contact { get; set; }
        public HrmEmployeeEmployment? Employment { get; set; }
        public HrmEmployeePayroll? Payroll { get; set; }
        public HrmEmployeeVerification? Verification { get; set; }
        public HrmMedicalFitnessCheck? MedicalFitnessCheck { get; set; }

        // Navigation collections
        public ICollection<HrmEmployeeDocument> Documents { get; set; } = new HashSet<HrmEmployeeDocument>();
        public ICollection<HrmEmployeeNominee> Nominees { get; set; } = new HashSet<HrmEmployeeNominee>();
        public ICollection<HrmEmployeeEducation> Educations { get; set; } = new HashSet<HrmEmployeeEducation>();
        public ICollection<HrmEmployeeExperience> Experiences { get; set; } = new HashSet<HrmEmployeeExperience>();
        public ICollection<HrmEmployeeTraining> Trainings { get; set; } = new HashSet<HrmEmployeeTraining>();
        public ICollection<HrmEmployeeFamily> FamilyMembers { get; set; } = new HashSet<HrmEmployeeFamily>();
        public ICollection<HrmEmployeeBankAccount> BankAccounts { get; set; } = new HashSet<HrmEmployeeBankAccount>();
        public ICollection<HrmEmployeeReporting> Reportings { get; set; } = new HashSet<HrmEmployeeReporting>();
    }
}
