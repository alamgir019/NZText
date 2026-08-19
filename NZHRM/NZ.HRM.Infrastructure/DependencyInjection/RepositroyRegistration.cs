using Microsoft.Extensions.DependencyInjection;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Infrastructure.Repositories;

namespace NZ.HRM.Infrastructure.DependencyInjection
{
    public static class RepositroyRegistration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IMenuPermissionRepository, MenuPermissionRepository>();
            services.AddScoped<ISubUnitRepository, SubUnitRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();
            services.AddScoped<ICompanyLocationRepository, CompanyLocationRepository>();
            services.AddScoped<IComplexUnitDepartmentRepository, ComplexUnitDepartmentRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDepartmentSectionRepository, DepartmentSectionRepository>();
            services.AddScoped<IGradeRepository, GradeRepository>();
            services.AddScoped<IShiftRepository, ShiftRepository>();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<ISectionCellRepository, SectionCellRepository>();
            services.AddScoped<IDesignationRepository, DesignationRepository>();
            services.AddScoped<ICellRepository, CellRepository>();
            services.AddScoped<IEmployeeMasterRepository, EmployeeMasterRepository>();
            services.AddScoped<IEmployeePersonalRepository, EmployeePersonalRepository>();
            services.AddScoped<IEmployeeVerificationRepository, EmployeeVerificationRepository>();
            services.AddScoped<IPayrollRepository, PayrollRepository>();
            services.AddScoped<IDivisionRepository, DivisionRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IThanaRepository, ThanaRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IGroupComplexRepository, GroupComplexRepository>();
            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<IGroupComplexRepository, GroupComplexRepository>();
            services.AddScoped<IMedicalFitnessCheckRepository, MedicalFitnessCheckRepository>();
            services.AddScoped<IPhysicalExaminationSettingRepository, PhysicalExaminationSettingRepository>();
            // IRawPunchRepository and IProcessedPunchRepository are registered by AddAttendanceModule()
            services.AddScoped<IEmployeeEmploymentRepository, EmployeeEmploymentRepository>();
            services.AddScoped<IEmployeeDocumentRepository, EmployeeDocumentRepository>();
            services.AddScoped<IEmployeeSalaryAccountRepository, EmployeeSalaryAccountRepository>();
            services.AddScoped<IEmployeeNomineeRepository, EmployeeNomineeRepository>();
            // Security repositories
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            return services;
        }
    }
}
