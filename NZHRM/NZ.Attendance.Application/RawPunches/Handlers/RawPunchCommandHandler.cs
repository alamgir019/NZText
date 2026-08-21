using NZ.Attendance.Domain.Services;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.RawPunches.Commands.CreateRawPunch;
using NZ.HRM.Domain.Entities;

namespace NZ.Attendance.Application.RawPunches.Handlers;

public class RawPunchCommandHandler
{
    private readonly IRawPunchRepository _rawPunchRepository;
    private readonly IProcessedPunchRepository _processedPunchRepository;
    private readonly IShiftRepository _shiftRepository;
    private readonly PunchProcessingService _punchProcessingService;

    public RawPunchCommandHandler(
        IRawPunchRepository rawPunchRepository,
        IProcessedPunchRepository processedPunchRepository,
        IShiftRepository shiftRepository,
        PunchProcessingService punchProcessingService)
    {
        _rawPunchRepository = rawPunchRepository;
        _processedPunchRepository = processedPunchRepository;
        _shiftRepository = shiftRepository;
        _punchProcessingService = punchProcessingService;
    }

    public async Task<CreateRawPunchResultDto> Handle(CreateRawPunchCommand command, CancellationToken cancellationToken = default)
    {
        // 1. Save the raw punch
        var rawPunch = new AttRawPunch
        {
            EmployeeId = command.EmployeeId,
            PunchDate = command.PunchDate,
            PunchTime = command.PunchTime,
            PunchType = command.PunchType,
            DeviceId = command.DeviceId,
        };

        await _rawPunchRepository.AddAsync(rawPunch, cancellationToken);

        // 2. Get all active shifts
        var shifts = await _shiftRepository.GetAllAsync(false, cancellationToken);

        // 3. Process the punch (shift-snapping + randomization)
        var (matchedShift, adjustedTime, determinedPunchType) = _punchProcessingService.ProcessPunch(command.PunchTime, shifts);

        // 4. Save the processed punch
        var processedPunch = new AttProcessedPunch
        {
            EmployeeId = command.EmployeeId,
            RawPunchId = rawPunch.Id,
            ShiftId = matchedShift?.Id,
            PunchDate = command.PunchDate,
            RawPunchTime = command.PunchTime,
            AdjustedPunchTime = adjustedTime,
            PunchType = determinedPunchType
        };

        await _processedPunchRepository.AddAsync(processedPunch, cancellationToken);

        // 5. Return result
        return new CreateRawPunchResultDto
        {
            RawPunchId = rawPunch.Id,
            ProcessedPunchId = processedPunch.Id,
            RawPunchTime = command.PunchTime,
            AdjustedPunchTime = adjustedTime,
            PunchType = determinedPunchType,
            ShiftName = matchedShift?.ShiftName
        };
    }
}
