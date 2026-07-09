using NZ.HRM.Application.Banks.Dto;
using NZ.HRM.Application.Banks.Queries.GetAllBanks;
using NZ.HRM.Application.Banks.Queries.GetBankById;
using NZ.HRM.Application.Interfaces.Repositories;

namespace NZ.HRM.Application.Banks.Handlers;

public class BankQueryHandler
{
    private readonly IBankRepository _bankRepository;

    public BankQueryHandler(IBankRepository bankRepository)
    {
        _bankRepository = bankRepository;
    }

    public async Task<List<BankDto>> Handle(GetAllBanksQuery query, CancellationToken cancellationToken = default)
    {
        var banks = await _bankRepository.GetAllAsync(query.IncludeInactive, cancellationToken);

        return banks.Select(b => new BankDto
        {
            Id = b.Id,
            BankingCode = b.BankingCode,
            BankingName = b.BankingName,
            MobileBankingFlag = b.MobileBankingFlag,
            IsActive = b.IsActive
        }).ToList();
    }

    public async Task<BankDto?> Handle(GetBankByIdQuery query, CancellationToken cancellationToken = default)
    {
        var bank = await _bankRepository.GetByIdAsync(query.Id, cancellationToken);
        if (bank == null) return null;

        return new BankDto
        {
            Id = bank.Id,
            BankingCode = bank.BankingCode,
            BankingName = bank.BankingName,
            MobileBankingFlag = bank.MobileBankingFlag,
            IsActive = bank.IsActive
        };
    }
}
