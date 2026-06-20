using NZ.HRM.Application.FinancialDetails.Queries.GetAllFinancialDetails;
using NZ.HRM.Application.FinancialDetails.Queries.GetFinancialDetailById;
using NZ.HRM.Application.Interfaces.Repositories;

namespace NZ.HRM.Application.FinancialDetails.Handlers;

public class FinancialDetailQueryHandler
{
    private readonly IFinancialDetailRepository _financialDetailRepository;

    public FinancialDetailQueryHandler(IFinancialDetailRepository financialDetailRepository)
    {
        _financialDetailRepository = financialDetailRepository;
    }

    public async Task<List<FinancialDetailDto>> Handle(GetAllFinancialDetailsQuery query, CancellationToken cancellationToken = default)
    {
        List<NZ.HRM.Domain.Entities.HrmEmployeePayroll> details;

        //if (!string.IsNullOrEmpty(query.EmployeeId))
        //    details = await _financialDetailRepository.GetByEmployeeIdAsync(query.EmployeeId, query.IncludeInactive, cancellationToken);
        //else
        //    details = await _financialDetailRepository.GetAllAsync(query.IncludeInactive, cancellationToken);

        //return details.Select(x => new FinancialDetailDto
        //{
        //    Id = x.Id,
        //    EmployeeId = x.EmployeeId,
        //    BasicSalary = x.BasicSalary,
        //    HouseRentAllowance = x.HouseRentAllowance,
        //    MedicalAllowance = x.MedicalAllowance,
        //    ConveyanceAllowance = x.ConveyanceAllowance,
        //    OtherAllowance = x.OtherAllowance,
        //    GrossSalary = x.GrossSalary,
        //    PaymentMethod = x.PaymentMethod,
        //    BankName = x.BankName,
        //    BankAccountNo = x.BankAccountNo,
        //    AccountType = x.AccountType,
        //    Branch = x.Branch,
        //    TinNumber = x.TinNumber,
        //    IsTaxable = x.IsTaxable,
        //    TaxExempted = x.TaxExempted,
        //    NidNumber = x.NidNumber,
        //    IsProvidentFundApplicable = x.IsProvidentFundApplicable,
        //    PfAccountNo = x.PfAccountNo,
        //    IsGratuityApplicable = x.IsGratuityApplicable,
        //    IsEsiApplicable = x.IsEsiApplicable,
        //    SalaryEffectiveFrom = x.SalaryEffectiveFrom,
        //    Remarks = x.Remarks,
        //    CreatedOn = x.CreatedOn,
        //    CreatedBy = x.CreatedBy,
        //    UpdatedOn = x.UpdatedOn,
        //    UpdatedBy = x.UpdatedBy,
        //    IsActive = x.IsActive
        //}).ToList();
        return new List<FinancialDetailDto>();
    }

    public async Task<FinancialDetailDetailDto?> Handle(GetFinancialDetailByIdQuery query, CancellationToken cancellationToken = default)
    {
        //var x = await _financialDetailRepository.GetByIdAsync(query.Id, cancellationToken);
        //if (x == null)
        //    return null;

        //return new FinancialDetailDetailDto
        //{
        //    Id = x.Id,
        //    EmployeeId = x.EmployeeId,
        //    BasicSalary = x.BasicSalary,
        //    HouseRentAllowance = x.HouseRentAllowance,
        //    MedicalAllowance = x.MedicalAllowance,
        //    ConveyanceAllowance = x.ConveyanceAllowance,
        //    OtherAllowance = x.OtherAllowance,
        //    GrossSalary = x.GrossSalary,
        //    PaymentMethod = x.PaymentMethod,
        //    BankName = x.BankName,
        //    BankAccountNo = x.BankAccountNo,
        //    AccountType = x.AccountType,
        //    Branch = x.Branch,
        //    TinNumber = x.TinNumber,
        //    IsTaxable = x.IsTaxable,
        //    TaxExempted = x.TaxExempted,
        //    NidNumber = x.NidNumber,
        //    IsProvidentFundApplicable = x.IsProvidentFundApplicable,
        //    PfAccountNo = x.PfAccountNo,
        //    IsGratuityApplicable = x.IsGratuityApplicable,
        //    IsEsiApplicable = x.IsEsiApplicable,
        //    SalaryEffectiveFrom = x.SalaryEffectiveFrom,
        //    Remarks = x.Remarks,
        //    CreatedOn = x.CreatedOn,
        //    CreatedBy = x.CreatedBy,
        //    UpdatedOn = x.UpdatedOn,
        //    UpdatedBy = x.UpdatedBy,
        //    IsActive = x.IsActive
        //};
        return null;
    }
}
