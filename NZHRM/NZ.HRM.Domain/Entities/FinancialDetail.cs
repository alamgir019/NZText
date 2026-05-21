using NZ.HRM.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZ.HRM.Domain.Entities;

public class FinancialDetail : BaseEntity
{
    [Required]
    public string EmployeeId { get; set; } = string.Empty;

    [ForeignKey(nameof(EmployeeId))]
    public EmployeeMaster? Employee { get; set; }

    [Column(TypeName = "numeric(18,2)")]
    public decimal BasicSalary { get; set; }

    [Column(TypeName = "numeric(18,2)")]
    public decimal HouseRentAllowance { get; set; }

    [Column(TypeName = "numeric(18,2)")]
    public decimal MedicalAllowance { get; set; }

    [Column(TypeName = "numeric(18,2)")]
    public decimal ConveyanceAllowance { get; set; }

    [Column(TypeName = "numeric(18,2)")]
    public decimal OtherAllowance { get; set; }

    [Column(TypeName = "numeric(18,2)")]
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
