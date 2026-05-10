namespace NZ.HRM.Application.Cells.Queries.GetAllCells;

public class CellDto
{
    public string Id { get; set; } = string.Empty;
    public string NameEnglish { get; set; } = string.Empty;
    public string? NameBangla { get; set; }
    public string SectionId { get; set; } = string.Empty;
    public string SectionName { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime UpdatedOn { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
