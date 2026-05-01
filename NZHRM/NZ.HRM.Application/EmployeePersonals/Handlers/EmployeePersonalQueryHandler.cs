using NZ.HRM.Application.EmployeePersonals.Queries.GetAllEmployeePersonals;
using NZ.HRM.Application.EmployeePersonals.Queries.GetEmployeePersonalById;
using NZ.HRM.Application.Interfaces.Repositories;

namespace NZ.HRM.Application.EmployeePersonals.Handlers;

public class EmployeePersonalQueryHandler
{
    private readonly IEmployeePersonalRepository _employeePersonalRepository;

    public EmployeePersonalQueryHandler(IEmployeePersonalRepository employeePersonalRepository)
    {
        _employeePersonalRepository = employeePersonalRepository;
    }

    public async Task<List<EmployeePersonalDto>> Handle(GetAllEmployeePersonalsQuery query, CancellationToken cancellationToken = default)
    {
        List<NZ.HRM.Domain.Entities.EmployeePersonal> employeePersonals;

        if (!string.IsNullOrEmpty(query.EmployeeId))
        {
            var single = await _employeePersonalRepository.GetByEmployeeIdAsync(query.EmployeeId, cancellationToken);
            employeePersonals = single != null ? new List<NZ.HRM.Domain.Entities.EmployeePersonal> { single } : new List<NZ.HRM.Domain.Entities.EmployeePersonal>();
        }
        else
        {
            employeePersonals = await _employeePersonalRepository.GetAllAsync(cancellationToken);
        }

        return employeePersonals.Select(ep => new EmployeePersonalDto
        {
            Id = ep.Id,
            EmployeeId = ep.EmployeeId,
            EmployeeCode = ep.EmployeeCode,
            EmployeeNameEnglish = ep.Employee?.EmployeeNameEnglish ?? string.Empty,
            DateOfBirth = ep.DateOfBirth,
            Gender = ep.Gender,
            MaritalStatus = ep.MaritalStatus,
            MobileNumber = ep.MobileNumber,
            EmailAddress = ep.EmailAddress,
            DocumentType = ep.DocumentType,
            DocumentNumber = ep.DocumentNumber,
            BloodGroup = ep.BloodGroup,
            Religion = ep.Religion,
            Nationality = ep.Nationality,
            FatherNameEnglish = ep.FatherNameEnglish,
            FatherNameBangla = ep.FatherNameBangla,
            MotherNameEnglish = ep.MotherNameEnglish,
            MotherNameBangla = ep.MotherNameBangla,
            SpouseName = ep.SpouseName,
            SpouseMobile = ep.SpouseMobile,
            TinNumber = ep.TinNumber,
            EmployeeReference = ep.EmployeeReference
        }).ToList();
    }

    public async Task<EmployeePersonalDetailDto?> Handle(GetEmployeePersonalByIdQuery query, CancellationToken cancellationToken = default)
    {
        var employeePersonal = await _employeePersonalRepository.GetByIdAsync(query.Id, cancellationToken);

        if (employeePersonal == null)
            return null;

        return new EmployeePersonalDetailDto
        {
            Id = employeePersonal.Id,
            EmployeeId = employeePersonal.EmployeeId,
            EmployeeCode = employeePersonal.EmployeeCode,
            EmployeeNameEnglish = employeePersonal.Employee?.EmployeeNameEnglish ?? string.Empty,
            DateOfBirth = employeePersonal.DateOfBirth,
            Gender = employeePersonal.Gender,
            MaritalStatus = employeePersonal.MaritalStatus,
            MobileNumber = employeePersonal.MobileNumber,
            EmailAddress = employeePersonal.EmailAddress,
            DocumentType = employeePersonal.DocumentType,
            DocumentNumber = employeePersonal.DocumentNumber,
            BloodGroup = employeePersonal.BloodGroup,
            Religion = employeePersonal.Religion,
            Nationality = employeePersonal.Nationality,
            FatherNameEnglish = employeePersonal.FatherNameEnglish,
            FatherNameBangla = employeePersonal.FatherNameBangla,
            MotherNameEnglish = employeePersonal.MotherNameEnglish,
            MotherNameBangla = employeePersonal.MotherNameBangla,
            SpouseName = employeePersonal.SpouseName,
            SpouseMobile = employeePersonal.SpouseMobile,
            TinNumber = employeePersonal.TinNumber,
            EmployeeReference = employeePersonal.EmployeeReference,
            CreatedOn = employeePersonal.CreatedOn,
            CreatedBy = employeePersonal.CreatedBy,
            UpdatedOn = employeePersonal.UpdatedOn,
            UpdatedBy = employeePersonal.UpdatedBy
        };
    }
}
