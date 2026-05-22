using NZ.HRM.Application.CompanyLocations.Commands.CreateCompanyLocation;
using NZ.HRM.Application.CompanyLocations.Commands.DeleteCompanyLocation;
using NZ.HRM.Application.CompanyLocations.Commands.UpdateCompanyLocation;
using NZ.HRM.Application.Interface;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.CompanyLocations.Handlers;

public class CompanyLocationCommandHandler
{
    private readonly ICompanyLocationRepository _companyLocationRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly ILocationRepository _locationRepository;

    public CompanyLocationCommandHandler(
        ICompanyLocationRepository companyLocationRepository,
        ICompanyRepository companyRepository,
        ILocationRepository locationRepository)
    {
        _companyLocationRepository = companyLocationRepository;
        _companyRepository = companyRepository;
        _locationRepository = locationRepository;
    }

    public async Task<string> Handle(CreateCompanyLocationCommand command, CancellationToken cancellationToken = default)
    {
        var companyExists = await _companyRepository.ExistsAsync(command.CompanyId, cancellationToken);
        if (!companyExists)
            throw new KeyNotFoundException($"Company with ID {command.CompanyId} not found");

        var location = await _locationRepository.FindByIdAsync(command.LocationId);
        if (location == null)
            throw new KeyNotFoundException($"Location with ID {command.LocationId} not found");

        var companyLocation = new CompanyLocation
        {
            CompanyId = command.CompanyId,
            LocationId = command.LocationId,
            IsActive = true
        };

        return await _companyLocationRepository.AddAsync(companyLocation, cancellationToken);
    }

    public async Task Handle(UpdateCompanyLocationCommand command, CancellationToken cancellationToken = default)
    {
        var companyLocation = await _companyLocationRepository.GetByIdAsync(command.Id, cancellationToken);
        if (companyLocation == null)
            throw new KeyNotFoundException($"CompanyLocation with ID {command.Id} not found");

        var companyExists = await _companyRepository.ExistsAsync(command.CompanyId, cancellationToken);
        if (!companyExists)
            throw new KeyNotFoundException($"Company with ID {command.CompanyId} not found");

        var location = await _locationRepository.FindByIdAsync(command.LocationId);
        if (location == null)
            throw new KeyNotFoundException($"Location with ID {command.LocationId} not found");

        companyLocation.CompanyId = command.CompanyId;
        companyLocation.LocationId = command.LocationId;

        await _companyLocationRepository.UpdateAsync(companyLocation, cancellationToken);
    }

    public async Task Handle(DeleteCompanyLocationCommand command, CancellationToken cancellationToken = default)
    {
        var companyLocation = await _companyLocationRepository.GetByIdAsync(command.Id, cancellationToken);
        if (companyLocation == null)
            throw new KeyNotFoundException($"CompanyLocation with ID {command.Id} not found");

        companyLocation.IsActive = false;
        await _companyLocationRepository.UpdateAsync(companyLocation, cancellationToken);
    }
}
