using NZ.HRM.Application.Districts.Commands.CreateDistrict;
using NZ.HRM.Application.Districts.Commands.DeleteDistrict;
using NZ.HRM.Application.Districts.Commands.UpdateDistrict;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Districts.Handlers;

public class DistrictCommandHandler
{
    private readonly IDistrictRepository _districtRepository;
    private readonly IDivisionRepository _divisionRepository;

    public DistrictCommandHandler(IDistrictRepository districtRepository, IDivisionRepository divisionRepository)
    {
        _districtRepository = districtRepository;
        _divisionRepository = divisionRepository;
    }

    public async Task<string> Handle(CreateDistrictCommand command, CancellationToken cancellationToken = default)
    {
        // validate division
        var divisionExists = await _divisionRepository.ExistsAsync(command.DivisionId, cancellationToken);
        if (!divisionExists) throw new KeyNotFoundException($"Division with ID {command.DivisionId} not found");

        var district = new LookDistrict
        {
            DistrictName = command.DistrictName,
            DistrictNameBangla = command.DistrictNameBangla,
            DivisionId = command.DivisionId,
            IsActive = true
        };

        return await _districtRepository.AddAsync(district, cancellationToken);
    }

    public async Task Handle(UpdateDistrictCommand command, CancellationToken cancellationToken = default)
    {
        var district = await _districtRepository.GetByIdAsync(command.Id, cancellationToken);
        if (district == null) throw new KeyNotFoundException($"District with ID {command.Id} not found");

        if (!string.IsNullOrWhiteSpace(command.DivisionId))
        {
            var divisionExists = await _divisionRepository.ExistsAsync(command.DivisionId, cancellationToken);
            if (!divisionExists) throw new KeyNotFoundException($"Division with ID {command.DivisionId} not found");
            district.DivisionId = command.DivisionId!;
        }

        district.DistrictName = command.DistrictName;
        district.DistrictNameBangla = command.DistrictNameBangla;

        await _districtRepository.UpdateAsync(district, cancellationToken);
    }

    public async Task Handle(DeleteDistrictCommand command, CancellationToken cancellationToken = default)
    {
        var district = await _districtRepository.GetByIdAsync(command.Id, cancellationToken);
        if (district == null) throw new KeyNotFoundException($"District with ID {command.Id} not found");

        district.IsActive = false;
        await _districtRepository.UpdateAsync(district, cancellationToken);
    }
}
