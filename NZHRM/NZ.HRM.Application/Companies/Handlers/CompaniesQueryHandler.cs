using NZ.HRM.Application.Companies.Queries.GetAllCompanies;
using NZ.HRM.Application.Companies.Queries.GetCompanyById;
using NZ.HRM.Application.Interfaces.Repositories;

namespace NZ.HRM.Application.Companies.Handlers;

public class CompaniesQueryHandler
{
    private readonly ICompanyRepository _companyRepository;

    public CompaniesQueryHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<List<CompanyDto>> Handle(GetAllCompaniesQuery query, CancellationToken cancellationToken = default)
    {
        var companies = await _companyRepository.GetAllAsync(query.IncludeInactive, cancellationToken);

        return companies.Select(c => new CompanyDto
        {
            Id = c.Id,
            CompanyCode = c.CompanyCode,
            CompanyName = c.CompanyName,
            CreatedOn = c.CreatedOn,
            CreatedBy = c.CreatedBy,
            UpdatedOn = c.UpdatedOn,
            UpdatedBy = c.UpdatedBy,
            IsActive = c.IsActive,
            IsCompliant = c.IsCompliant
        }).ToList();
    }
    public async Task<CompanyDetailDto?> Handle(GetCompanyByIdQuery query, CancellationToken cancellationToken = default)
    {
        var company = await _companyRepository.GetByIdAsync(query.Id, cancellationToken);

        if (company == null)
            return null;

        return new CompanyDetailDto
        {
            Id = company.Id,
            CompanyCode = company.CompanyCode,
            CompanyName = company.CompanyName,
            CreatedOn = company.CreatedOn,
            CreatedBy = company.CreatedBy,
            UpdatedOn = company.UpdatedOn,
            UpdatedBy = company.UpdatedBy,
            IsActive = company.IsActive,
            IsCompliant = company.IsCompliant
        };
    }
}