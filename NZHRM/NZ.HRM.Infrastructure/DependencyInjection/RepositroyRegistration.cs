using Microsoft.Extensions.DependencyInjection;
using NZ.HRM.Application.Interface;
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
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyLocationRepository, CompanyLocationRepository>();
            services.AddScoped<ILocationDepartmentRepository, LocationDepartmentRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDepartmentSectionRepository, DepartmentSectionRepository>();
            services.AddScoped<IGradeRepository, GradeRepository>();
            services.AddScoped<IShiftRepository, ShiftRepository>();
            services.AddScoped<IEmployeeNatureRepository, EmployeeNatureRepository>();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<ISectionCellRepository, SectionCellRepository>();
            services.AddScoped<IDesignationRepository, DesignationRepository>();
            services.AddScoped<ICellRepository, CellRepository>();
            services.AddScoped<IEmployeeMasterRepository, EmployeeMasterRepository>();
            services.AddScoped<IEmployeePersonalRepository, EmployeePersonalRepository>();
            services.AddScoped<IEmployeeVerificationRepository, EmployeeVerificationRepository>();
            services.AddScoped<IFinancialDetailRepository, FinancialDetailRepository>();
            services.AddScoped<IDivisionRepository, DivisionRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IThanaRepository, ThanaRepository>();
            services.AddScoped<IMedicalFitnessCheckRepository, MedicalFitnessCheckRepository>();
            services.AddScoped<IPhysicalExaminationSettingRepository, PhysicalExaminationSettingRepository>();
            services.AddScoped<IRawPunchRepository, RawPunchRepository>();
            services.AddScoped<IProcessedPunchRepository, ProcessedPunchRepository>();
            return services;
        }
    }
}
