using NZ.HRM.Application.Employees.Queries.GetEmployeeConfirmationDate;
using NZ.HRM.Application.Employees.Queries.GetEmployeeDetail;
using NZ.HRM.Application.Employees.Queries.GetEmployeesByStatus;
using NZ.HRM.Application.Employees.Queries.SearchEmployees;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.Model.Employees.DTOs;
using NZ.HRM.Mapping.Employees;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.Employees.Handlers;

public class CompleteEmployeeQueryHandler
{
    private readonly IEmployeeMasterRepository _employeeMasterRepository;

    public CompleteEmployeeQueryHandler(IEmployeeMasterRepository employeeMasterRepository)
    {
        _employeeMasterRepository = employeeMasterRepository;
    }

    public async Task<EmployeeDetailDto?> Handle(GetEmployeeDetailQuery query, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeMasterRepository.GetByIdAsync(query.EmployeeId, cancellationToken);

        if (employee == null)
            return null;

        return employee.MapToEmployeeDetailDto();
    }

    public async Task<List<EmployeeSearchDto>> Handle(SearchEmployeesQuery query, CancellationToken cancellationToken = default)
    {
        var employees = await _employeeMasterRepository.SearchAsync(query.SearchText, cancellationToken);

        return employees.Select(x => 
        new EmployeeSearchDto() {
            Id = x.Id,
            EmployeeNameEnglish = x.EmployeeName,
            EmployeeNameBangla = x.EmployeeNameBangla,
            EnrollmentId = x.EnrollmentId,
            //MobileNumber = x.PersonalInfo?.MobileNumber,
        }).ToList();
    }

    public async Task<DateTime?> Handle(GetEmployeeConfirmationDateQuery query, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeMasterRepository.GetByIdAsync(query.EmployeeId, cancellationToken);
        if (employee == null)
            return null;

        //var probationMonths = employee.EmployeeType == EmployeeType.Worker ? 3 : 6;
        //return query.JoiningDate.AddMonths(probationMonths).Date;
        return null;
    }

    public async Task<List<EmployeeByStatusDto>> Handle(GetEmployeesByStatusQuery query, CancellationToken cancellationToken = default)
    {
        var employees = await _employeeMasterRepository.GetByStatusAsync(query.Status, cancellationToken);

        return employees.Select(x => new EmployeeByStatusDto
        {
            EmployeeId = x.Id,
            EnrollmentId = x.EnrollmentId,
            EmployeeName = string.IsNullOrWhiteSpace(x.EmployeeName) ? x.EmployeeNameBangla : x.EmployeeName,
            Age = CalculateAge(x.Personal?.DateOfBirth),
            ExaminationDate = x.MedicalFitnessCheck?.ExaminationDateTime,
            DateOfJoining = x.Employment?.JoiningDate,
            DateOfBirth = x.Personal?.DateOfBirth,
            BloodGroup = !string.IsNullOrWhiteSpace(x.Personal?.BloodGroup) && Enum.TryParse<BloodGroup>(x.Personal.BloodGroup, out var bloodGroup)
                ? bloodGroup
                : null,
            Gender = !string.IsNullOrWhiteSpace(x.Personal?.Gender) && Enum.TryParse<Gender>(x.Personal.Gender, out var gender)
                ? gender
                : null,
            Department = x.Employment?.Department?.DepartmentName,
            Section = x.Employment?.Section?.SectionName,
            Cell = x.Employment?.Cell?.CellName,
            Designation = x.Employment?.Designation?.DesignationName,
            Grade = x.Employment?.Grade?.GradeName,
            Shift = x.Employment?.Shift?.ShiftName,
            WeekOffDay = !string.IsNullOrWhiteSpace(x.Employment?.WeeklyOffDay) && Enum.TryParse<WeekOffDay>(x.Employment.WeeklyOffDay, out var weekOffDay)
                ? weekOffDay
                : null, 
            ProposedSalary = x.Payroll?.ProposedSalary
        }).ToList();
    }

    private static int? CalculateAge(DateOnly? dateOfBirth)
    {
        if (!dateOfBirth.HasValue)
        {
            return null;
        }

        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - dateOfBirth.Value.Year;

        if (dateOfBirth.Value > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}
