namespace NZ.HRM.Application.Banks.Dto;

public class BankDto
{
    public string Id { get; set; } = string.Empty;
    public string BankingCode { get; set; } = string.Empty;
    public string BankingName { get; set; } = string.Empty;
    public bool MobileBankingFlag { get; set; }
    public bool IsActive { get; set; }
}
