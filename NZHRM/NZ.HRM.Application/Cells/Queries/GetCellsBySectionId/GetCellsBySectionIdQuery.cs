using NZ.HRM.Application.Cells.Queries.GetAllCells;

namespace NZ.HRM.Application.Cells.Queries.GetCellsBySectionId
{
    public class GetCellsBySectionIdQuery : GetAllCellsQuery
    {
        public string SectionId { get; set; } = string.Empty;
    }
}