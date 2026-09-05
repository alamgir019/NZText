namespace NZ.HRM.Application.Common;

/// <summary>
/// Raised when a business rule/validation defined by the domain is violated.
/// </summary>
public class BusinessRuleException : Exception
{
    public string Code { get; }

    public BusinessRuleException(string code, string message) : base(message) => Code = code;
}
