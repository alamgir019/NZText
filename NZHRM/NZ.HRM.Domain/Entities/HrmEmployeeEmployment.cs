using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("employee_employment", Schema = "hrm")]
    public class HrmEmployeeEmployment : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty; // FK to employee_master.Id

        public DateOnly? JoiningDate { get; set; }
        public DateOnly? ConfirmationDate { get; set; }
        public DateOnly? ResignationDate { get; set; }
        public DateOnly? SeparationDate { get; set; }

        public string? GroupId { get; set; }
        public string? UnitId { get; set; }
        public string? SubunitId { get; set; }
        public string? DepartmentId { get; set; }
        public string? SectionId { get; set; }
        public string? CellId { get; set; }
        public string? DesignationId { get; set; }
        public string? GradeId { get; set; }
        public string? ShiftId { get; set; }
        public string? EmployeeCategoryId { get; set; }
        public string? ReportingEmployeeId { get; set; }
        public string? ProcessingGroupId { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
        [ForeignKey("GroupId")] public MstGroup? Group { get; set; }
        [ForeignKey("UnitId")] public MstUnit? Unit { get; set; }
        [ForeignKey("SubunitId")] public MstSubunit? Subunit { get; set; }
        [ForeignKey("DepartmentId")] public MstDepartment? Department { get; set; }
        [ForeignKey("SectionId")] public MstSection? Section { get; set; }
        [ForeignKey("CellId")] public MstCell? Cell { get; set; }
        [ForeignKey("DesignationId")] public MstDesignation? Designation { get; set; }
        [ForeignKey("GradeId")] public MstGrade? Grade { get; set; }
        [ForeignKey("ShiftId")] public MstShift? Shift { get; set; }
        [ForeignKey("EmployeeCategoryId")] public MstEmployeeCategory? EmployeeCategory { get; set; }
        [ForeignKey("ReportingEmployeeId")] public HrmEmployeeMaster? ReportingEmployee { get; set; }
        [ForeignKey("ProcessingGroupId")] public MstPayrollProcessingGroup? ProcessingGroup { get; set; }
    }
}
