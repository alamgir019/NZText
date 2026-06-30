using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.Model.Employees.DTOs
{
    public class EmployeeDocumentDto
    {
        public string? EmployeeId { get; set; }
        public DocumentType? DocumentType { get; set; }
        public string? DocumentNo { get; set; }
        public DateOnly? IssueDate { get; set; }
        public DateOnly? ExpiryDate { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
    }
}
