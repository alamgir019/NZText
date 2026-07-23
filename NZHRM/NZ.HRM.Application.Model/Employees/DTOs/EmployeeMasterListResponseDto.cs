namespace NZ.HRM.Application.Model.Employees.DTOs;

public class EmployeeMasterListResponseDto
{
    /// <summary>
    /// List of employees
    /// </summary>
    public List<EmployeeMasterListItemDto> Employees { get; set; } = new();

    /// <summary>
    /// Total count of matching records (before pagination)
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Current page number
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Page size
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Total number of pages
    /// </summary>
    public int TotalPages => (TotalCount + PageSize - 1) / PageSize;

    /// <summary>
    /// Has next page
    /// </summary>
    public bool HasNextPage => PageNumber < TotalPages;

    /// <summary>
    /// Has previous page
    /// </summary>
    public bool HasPreviousPage => PageNumber > 1;
}
