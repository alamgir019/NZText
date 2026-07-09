using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.Banks.Commands.UpdateBank;

public class UpdateBankCommand
{
    [Required]
    public string Id { get; set; } = string.Empty;

    [Required(ErrorMessage = "Bank name is required")]
    [MaxLength(200, ErrorMessage = "Bank name must not exceed 200 characters")]
    public string BankingName { get; set; } = string.Empty;

    [MaxLength(50, ErrorMessage = "Bank code must not exceed 50 characters")]
    public string? BankingCode { get; set; }

    public bool MobileBankingFlag { get; set; }
}
