namespace NZ.HRM.Application.FinancialDetails.Queries.GetAllFinancialDetails;

public class FinancialDetailDto
{
    public string Id { get; set; } = string.Empty;
    public string EmployeeId { get; set; } = string.Empty;
    public decimal BasicSalary { get; set; }
    public decimal HouseRentAllowance { get; set; }
    public decimal MedicalAllowance { get; set; }
    public decimal ConveyanceAllowance { get; set; }
    public decimal OtherAllowance { get; set; }
    public decimal GrossSalary { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string BankName { get; set; } = string.Empty;
    public string BankAccountNo { get; set; } = string.Empty;
    public string? AccountType { get; set; }
    public string? Branch { get; set; }
    public string? TinNumber { get; set; }
    public bool IsTaxable { get; set; }
    public string? TaxExempted { get; set; }
    public string? NidNumber { get; set; }
    public bool IsProvidentFundApplicable { get; set; }
    public string? PfAccountNo { get; set; }
    public bool IsGratuityApplicable { get; set; }
    public bool IsEsiApplicable { get; set; }
    public DateTime? SalaryEffectiveFrom { get; set; }
    public string? Remarks { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime UpdatedOn { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
