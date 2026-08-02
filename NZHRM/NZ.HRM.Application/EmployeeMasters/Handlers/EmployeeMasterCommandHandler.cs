using NZ.HRM.Application.EmployeeMasters.Commands.CreateEmployeeMaster;
using NZ.HRM.Application.EmployeeMasters.Commands.DeleteEmployeeMaster;
using NZ.HRM.Application.EmployeeMasters.Commands.UpdateEmployeeMaster;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.EmployeeMasters.Handlers;

public class EmployeeMasterCommandHandler
{
    private readonly IEmployeeMasterRepository _employeeMasterRepository;
    private readonly IUnitRepository _companyRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly ISectionRepository _sectionRepository;
    private readonly IGradeRepository _gradeRepository;
    private readonly IShiftRepository _shiftRepository;

    public EmployeeMasterCommandHandler(
        IEmployeeMasterRepository employeeMasterRepository,
        IUnitRepository companyRepository,
        IDepartmentRepository departmentRepository,
        ISectionRepository sectionRepository,
        IGradeRepository gradeRepository,
        IShiftRepository shiftRepository)
    {
        _employeeMasterRepository = employeeMasterRepository;
        _companyRepository = companyRepository;
        _departmentRepository = departmentRepository;
        _sectionRepository = sectionRepository;
        _gradeRepository = gradeRepository;
        _shiftRepository = shiftRepository;
    }
}
