using ClosedXML.Excel;
using NZ.HRM.Application.Model.Employees.DTOs;
using System.Drawing;

namespace NZ.HRM.WebAPI.Services;

public interface IEmployeeExcelExportService
{
    /// <summary>
    /// Generate Excel file from employee master list data.
    /// </summary>
    Task<byte[]> GenerateEmployeeMasterListExcelAsync(List<EmployeeMasterListItemDto> employees);
}

public class EmployeeExcelExportService : IEmployeeExcelExportService
{
    public async Task<byte[]> GenerateEmployeeMasterListExcelAsync(List<EmployeeMasterListItemDto> employees)
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Employee Master List");

            // Define headers
            var headers = new[]
            {
                "Employee ID",
                "Employee Code",
                "Employee Name",
                "Department",
                "Section",
                "Cell",
                "Designation",
                "Employee Nature",
                "Joining Date",
                "Active Status"
            };

            // Write headers
            for (int i = 0; i < headers.Length; i++)
            {
                var cell = worksheet.Cell(1, i + 1);
                cell.Value = headers[i];

                // Format header row
                cell.Style.Font.Bold = true;
                cell.Style.Font.FontColor = XLColor.White;
                cell.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 102, 204); // Blue background
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            }

            // Write data rows
            for (int i = 0; i < employees.Count; i++)
            {
                var employee = employees[i];
                int row = i + 2; // Start from row 2 (after headers)

                worksheet.Cell(row, 1).Value = employee.EmployeeId;
                worksheet.Cell(row, 2).Value = employee.EmployeeCode;
                worksheet.Cell(row, 3).Value = employee.EmployeeName;
                worksheet.Cell(row, 4).Value = employee.DepartmentName;
                worksheet.Cell(row, 5).Value = employee.SectionName;
                worksheet.Cell(row, 6).Value = employee.CellName;
                worksheet.Cell(row, 7).Value = employee.DesignationName;
                worksheet.Cell(row, 8).Value = employee.EmployeeNature;
                worksheet.Cell(row, 9).Value = employee.JoiningDate;
                worksheet.Cell(row, 10).Value = employee.IsActive ? "Active" : "Inactive";

                // Alternate row colors for better readability
                if (i % 2 == 0)
                {
                    for (int col = 1; col <= headers.Length; col++)
                    {
                        worksheet.Cell(row, col).Style.Fill.BackgroundColor = XLColor.FromArgb(242, 242, 242); // Light gray
                    }
                }
            }

            // Auto-fit columns
            worksheet.Columns().AdjustToContents(minWidth: 10, maxWidth: 50);

            // Add summary information
            int summaryRow = employees.Count + 4;

            worksheet.Cell(summaryRow, 1).Value = "Total Records:";
            worksheet.Cell(summaryRow, 2).Value = employees.Count;
            worksheet.Cell(summaryRow, 1).Style.Font.Bold = true;

            var activeCount = employees.Count(e => e.IsActive);
            worksheet.Cell(summaryRow + 1, 1).Value = "Active Employees:";
            worksheet.Cell(summaryRow + 1, 2).Value = activeCount;
            worksheet.Cell(summaryRow + 1, 1).Style.Font.Bold = true;

            var inactiveCount = employees.Count - activeCount;
            worksheet.Cell(summaryRow + 2, 1).Value = "Inactive Employees:";
            worksheet.Cell(summaryRow + 2, 2).Value = inactiveCount;
            worksheet.Cell(summaryRow + 2, 1).Style.Font.Bold = true;

            // Convert to byte array
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return await Task.FromResult(stream.ToArray());
            }
        }
    }
}

