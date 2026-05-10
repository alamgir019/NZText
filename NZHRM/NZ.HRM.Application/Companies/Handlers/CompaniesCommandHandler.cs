using NZ.HRM.Application.Companies.Commands.CreateCompany;
using NZ.HRM.Application.Companies.Commands.DeleteCompany;
using NZ.HRM.Application.Companies.Commands.UpdateCompany;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Companies.Handlers;

public class CompaniesCommandHandler
{
    private readonly ICompanyRepository _companyRepository;

    public CompaniesCommandHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task Handle(DeleteCompanyCommand command, CancellationToken cancellationToken = default)
    {
        var company = await _companyRepository.GetByIdAsync(command.Id, cancellationToken);

        if (company == null)
            throw new KeyNotFoundException($"Company with ID {command.Id} not found");

        // Soft delete
        company.IsActive = false;
        await _companyRepository.UpdateAsync(company, cancellationToken);

        // Or hard delete
        // await _companyRepository.DeleteAsync(company, cancellationToken);
    }

    public async Task Handle(UpdateCompanyCommand command, CancellationToken cancellationToken = default)
    {
        var company = await _companyRepository.GetByIdAsync(command.Id, cancellationToken);

        if (company == null)
            throw new KeyNotFoundException($"Company with ID {command.Id} not found");

        company.CompanyCode = command.CompanyCode;
        company.CompanyName = command.CompanyName;
        company.LocationId = command.LocationId;
        company.IsCompliant = command.IsCompliant;

        await _companyRepository.UpdateAsync(company, cancellationToken);
    }

    public async Task<string> Handle(CreateCompanyCommand command, CancellationToken cancellationToken = default)
    {
        var company = new Company
        {
            CompanyCode = command.CompanyCode,
            CompanyName = command.CompanyName,
            LocationId = command.LocationId,
            IsCompliant = command.IsCompliant,
            IsActive = true
        };

        return await _companyRepository.AddAsync(company, cancellationToken);
    }
}