namespace NZ.HRM.Application.SectionCells.Queries.GetAllSectionCells;

public class GetAllSectionCellsQuery
{
    public bool IncludeInactive { get; set; } = false;
    public string? SectionId { get; set; }
    public string? CellId { get; set; }
}
