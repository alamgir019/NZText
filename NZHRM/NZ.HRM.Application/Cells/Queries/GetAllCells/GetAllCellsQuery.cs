namespace NZ.HRM.Application.Cells.Queries.GetAllCells;

public class GetAllCellsQuery
{
    public bool IncludeInactive { get; set; } = false;
    public string? SectionId { get; set; }
}
