using NZ.HRM.Application.Thanas.Commands.CreateThana;
using NZ.HRM.Application.Thanas.Commands.DeleteThana;
using NZ.HRM.Application.Thanas.Commands.UpdateThana;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Thanas.Handlers;

public class ThanaCommandHandler
{
    private readonly IThanaRepository _thanaRepository;
    private readonly IDistrictRepository _districtRepository;

    public ThanaCommandHandler(IThanaRepository thanaRepository, IDistrictRepository districtRepository)
    {
        _thanaRepository = thanaRepository;
        _districtRepository = districtRepository;
    }

    public async Task<string> Handle(CreateThanaCommand command, CancellationToken cancellationToken = default)
    {
        var districtExists = await _districtRepository.ExistsAsync(command.DistrictId, cancellationToken);
        if (!districtExists) throw new KeyNotFoundException($"District with ID {command.DistrictId} not found");

        var thana = new LookThana
        {
            ThanaName = command.ThanaName,
            ThanaNameBangla = command.ThanaNameBangla,
            DistrictId = command.DistrictId,
            IsActive = true
        };

        return await _thanaRepository.AddAsync(thana, cancellationToken);
    }

    public async Task Handle(UpdateThanaCommand command, CancellationToken cancellationToken = default)
    {
        var thana = await _thanaRepository.GetByIdAsync(command.Id, cancellationToken);
        if (thana == null) throw new KeyNotFoundException($"Thana with ID {command.Id} not found");

        if (!string.IsNullOrWhiteSpace(command.DistrictId))
        {
            var districtExists = await _districtRepository.ExistsAsync(command.DistrictId, cancellationToken);
            if (!districtExists) throw new KeyNotFoundException($"District with ID {command.DistrictId} not found");
            thana.DistrictId = command.DistrictId!;
        }

        thana.ThanaName = command.ThanaName;
        thana.ThanaNameBangla = command.ThanaNameBangla;

        await _thanaRepository.UpdateAsync(thana, cancellationToken);
    }

    public async Task Handle(DeleteThanaCommand command, CancellationToken cancellationToken = default)
    {
        var thana = await _thanaRepository.GetByIdAsync(command.Id, cancellationToken);
        if (thana == null) throw new KeyNotFoundException($"Thana with ID {command.Id} not found");

        thana.IsActive = false;
        await _thanaRepository.UpdateAsync(thana, cancellationToken);
    }
}
