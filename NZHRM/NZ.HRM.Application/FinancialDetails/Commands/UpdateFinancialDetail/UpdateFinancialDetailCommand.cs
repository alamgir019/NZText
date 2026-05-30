using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.FinancialDetails.Commands.UpdateFinancialDetail;

public class UpdateFinancialDetailCommand
{
    [Required]
    public string Id { get; set; } = string.Empty;

    [Required]
    public string EmployeeId { get; set; } = string.Empty;

    public decimal BasicSalary { get; set; }
    public decimal HouseRentAllowance { get; set; }
    public decimal MedicalAllowance { get; set; }
    public decimal ConveyanceAllowance { get; set; }
    public decimal OtherAllowance { get; set; }
    public decimal GrossSalary { get; set; }

    [Required]
    [MaxLength(50)]
    public string PaymentMethod { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string BankName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string BankAccountNo { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? AccountType { get; set; }

    [MaxLength(100)]
    public string? Branch { get; set; }

    [MaxLength(30)]
    public string? TinNumber { get; set; }

    public bool IsTaxable { get; set; } = true;

    [MaxLength(100)]
    public string? TaxExempted { get; set; }

    [MaxLength(30)]
    public string? NidNumber { get; set; }

    public bool IsProvidentFundApplicable { get; set; }

    [MaxLength(50)]
    public string? PfAccountNo { get; set; }

    public bool IsGratuityApplicable { get; set; }
    public bool IsEsiApplicable { get; set; }

    public DateTime? SalaryEffectiveFrom { get; set; }

    [MaxLength(500)]
    public string? Remarks { get; set; }
}
