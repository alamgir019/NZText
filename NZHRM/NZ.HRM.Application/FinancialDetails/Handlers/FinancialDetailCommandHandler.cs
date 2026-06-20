using NZ.HRM.Application.FinancialDetails.Commands.CreateFinancialDetail;
using NZ.HRM.Application.FinancialDetails.Commands.DeleteFinancialDetail;
using NZ.HRM.Application.FinancialDetails.Commands.UpdateFinancialDetail;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.FinancialDetails.Handlers;

public class FinancialDetailCommandHandler
{
    private readonly IFinancialDetailRepository _financialDetailRepository;
    private readonly IEmployeeMasterRepository _employeeMasterRepository;

    public FinancialDetailCommandHandler(
        IFinancialDetailRepository financialDetailRepository,
        IEmployeeMasterRepository employeeMasterRepository)
    {
        _financialDetailRepository = financialDetailRepository;
        _employeeMasterRepository = employeeMasterRepository;
    }

    public async Task<string> Handle(CreateFinancialDetailCommand command, CancellationToken cancellationToken = default)
    {
        var employeeExists = await _employeeMasterRepository.ExistsAsync(command.EmployeeId, cancellationToken);
        if (!employeeExists)
            throw new KeyNotFoundException($"Employee with ID {command.EmployeeId} not found");

        var financialDetail = new HrmEmployeePayroll
        {
            EmployeeId = command.EmployeeId,
            //BasicSalary = command.BasicSalary,
            //HouseRentAllowance = command.HouseRentAllowance,
            //MedicalAllowance = command.MedicalAllowance,
            //ConveyanceAllowance = command.ConveyanceAllowance,
            //OtherAllowance = command.OtherAllowance,
            //GrossSalary = command.GrossSalary,
            //PaymentMethod = command.PaymentMethod,
            //BankName = command.BankName,
            BankAccountNo = command.BankAccountNo,
            //AccountType = command.AccountType,
            //Branch = command.Branch,
            //TinNumber = command.TinNumber,
            //IsTaxable = command.IsTaxable,
            //TaxExempted = command.TaxExempted,
            //NidNumber = command.NidNumber,
            //IsProvidentFundApplicable = command.IsProvidentFundApplicable,
            //PfAccountNo = command.PfAccountNo,
            //IsGratuityApplicable = command.IsGratuityApplicable,
            //IsEsiApplicable = command.IsEsiApplicable,
            //SalaryEffectiveFrom = command.SalaryEffectiveFrom,
            //Remarks = command.Remarks,
            IsActive = true
        };

        //return await _financialDetailRepository.AddAsync(financialDetail, cancellationToken);
        return null;
    }

    public async Task Handle(UpdateFinancialDetailCommand command, CancellationToken cancellationToken = default)
    {
        //var financialDetail = await _financialDetailRepository.GetByIdAsync(command.Id, cancellationToken);
        //if (financialDetail == null)
        //    throw new KeyNotFoundException($"Financial detail with ID {command.Id} not found");

        //var employeeExists = await _employeeMasterRepository.ExistsAsync(command.EmployeeId, cancellationToken);
        //if (!employeeExists)
        //    throw new KeyNotFoundException($"Employee with ID {command.EmployeeId} not found");

        //financialDetail.EmployeeId = command.EmployeeId;
        //financialDetail.BasicSalary = command.BasicSalary;
        //financialDetail.HouseRentAllowance = command.HouseRentAllowance;
        //financialDetail.MedicalAllowance = command.MedicalAllowance;
        //financialDetail.ConveyanceAllowance = command.ConveyanceAllowance;
        //financialDetail.OtherAllowance = command.OtherAllowance;
        //financialDetail.GrossSalary = command.GrossSalary;
        //financialDetail.PaymentMethod = command.PaymentMethod;
        //financialDetail.BankName = command.BankName;
        //financialDetail.BankAccountNo = command.BankAccountNo;
        //financialDetail.AccountType = command.AccountType;
        //financialDetail.Branch = command.Branch;
        //financialDetail.TinNumber = command.TinNumber;
        //financialDetail.IsTaxable = command.IsTaxable;
        //financialDetail.TaxExempted = command.TaxExempted;
        //financialDetail.NidNumber = command.NidNumber;
        //financialDetail.IsProvidentFundApplicable = command.IsProvidentFundApplicable;
        //financialDetail.PfAccountNo = command.PfAccountNo;
        //financialDetail.IsGratuityApplicable = command.IsGratuityApplicable;
        //financialDetail.IsEsiApplicable = command.IsEsiApplicable;
        //financialDetail.SalaryEffectiveFrom = command.SalaryEffectiveFrom;
        //financialDetail.Remarks = command.Remarks;

        //await _financialDetailRepository.UpdateAsync(financialDetail, cancellationToken);
    }

    public async Task Handle(DeleteFinancialDetailCommand command, CancellationToken cancellationToken = default)
    {
        //var financialDetail = await _financialDetailRepository.GetByIdAsync(command.Id, cancellationToken);
        //if (financialDetail == null)
        //    throw new KeyNotFoundException($"Financial detail with ID {command.Id} not found");

        //financialDetail.IsActive = false;
        //await _financialDetailRepository.UpdateAsync(financialDetail, cancellationToken);
    }
}
