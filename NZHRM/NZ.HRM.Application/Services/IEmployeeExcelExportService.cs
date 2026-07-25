using NZ.HRM.Application.Model.Employees.DTOs;

namespace NZ.HRM.Application.Services;

public interface IEmployeeExcelExportService
{
    /// <summary>
    /// Generate Excel file from employee master list data.
    /// </summary>
    Task<byte[]> GenerateEmployeeMasterListExcelAsync(List<EmployeeMasterListItemDto> employees);
}

