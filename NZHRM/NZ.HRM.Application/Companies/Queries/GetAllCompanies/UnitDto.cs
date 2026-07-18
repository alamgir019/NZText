namespace NZ.HRM.Application.Units.Queries.GetAllUnits;

public class UnitDto
{
    public string Id { get; set; } = string.Empty;
    public string UnitCode { get; set; } = string.Empty;
    public string UnitName { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime UpdatedOn { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsCompliant { get; set; }
    public string? UnitNameBangla { get; internal set; }
}