using ClosedXML.Excel;
using NZ.HRM.Application.Model.LearnerAdjustments.DTOs;

namespace NZ.HRM.Application.Services;

public class EligibleLearnerExcelExportService : IEligibleLearnerExcelExportService
{
    public Task<byte[]> GenerateEligibleLearnersExcelAsync(List<EligibleLearnerDto> learners)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Eligible Learners");

        var headers = new[]
        {
            "Employee ID",
            "Employee Name",
            "Department / Section",
            "Designation",
            "Date of Joining",
            "Probation Completed On",
            "Current Gross Salary",
            "Standard Gross Salary",
            "Adjustment Amount"
        };

        for (int i = 0; i < headers.Length; i++)
        {
            var cell = worksheet.Cell(1, i + 1);
            cell.Value = headers[i];

            cell.Style.Font.Bold = true;
            cell.Style.Font.FontColor = XLColor.White;
            cell.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 102, 204);
            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        }

        for (int i = 0; i < learners.Count; i++)
        {
            var learner = learners[i];
            int row = i + 2;

            worksheet.Cell(row, 1).Value = learner.EmployeeId;
            worksheet.Cell(row, 2).Value = learner.EmployeeName;
            worksheet.Cell(row, 3).Value = learner.DepartmentName;
            worksheet.Cell(row, 4).Value = learner.Designation;
            worksheet.Cell(row, 5).Value = learner.DateOfJoining.ToString("yyyy-MM-dd");
            worksheet.Cell(row, 6).Value = learner.ProbationCompletedOn.ToString("yyyy-MM-dd");
            worksheet.Cell(row, 7).Value = learner.CurrentGrossSalary;
            worksheet.Cell(row, 8).Value = learner.StandardGrossSalary;
            worksheet.Cell(row, 9).Value = learner.AdjustmentAmount;

            worksheet.Range(row, 7, row, 9).Style.NumberFormat.Format = "#,##0.00";
        }

        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return Task.FromResult(stream.ToArray());
    }
}
