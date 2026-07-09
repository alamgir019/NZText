using NZ.HRM.Application.Banks.Commands.CreateBank;
using NZ.HRM.Application.Banks.Commands.UpdateBank;
using NZ.HRM.Application.Banks.Commands.DeleteBank;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Banks.Handlers;

public class BankCommandHandler
{
    private readonly IBankRepository _bankRepository;

    public BankCommandHandler(IBankRepository bankRepository)
    {
        _bankRepository = bankRepository;
    }

    public async Task<string> Handle(CreateBankCommand command, CancellationToken cancellationToken = default)
    {
        var bank = new LookBanking
        {
            BankingCode = command.BankingCode ?? string.Empty,
            BankingName = command.BankingName,
            MobileBankingFlag = command.MobileBankingFlag,
            IsActive = true
        };

        return await _bankRepository.AddAsync(bank, cancellationToken);
    }

    public async Task Handle(UpdateBankCommand command, CancellationToken cancellationToken = default)
    {
        var bank = await _bankRepository.GetByIdAsync(command.Id, cancellationToken);
        if (bank == null) throw new KeyNotFoundException($"Bank with ID {command.Id} not found");

        bank.BankingCode = command.BankingCode ?? string.Empty;
        bank.BankingName = command.BankingName;
        bank.MobileBankingFlag = command.MobileBankingFlag;

        await _bankRepository.UpdateAsync(bank, cancellationToken);
    }

    public async Task Handle(DeleteBankCommand command, CancellationToken cancellationToken = default)
    {
        var bank = await _bankRepository.GetByIdAsync(command.Id, cancellationToken);
        if (bank == null) throw new KeyNotFoundException($"Bank with ID {command.Id} not found");

        bank.IsActive = false;
        await _bankRepository.UpdateAsync(bank, cancellationToken);
    }
}
