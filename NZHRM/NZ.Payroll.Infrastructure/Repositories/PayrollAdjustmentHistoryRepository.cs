using Microsoft.EntityFrameworkCore;
using NZ.HRM.Domain.Entities;
using NZ.Payroll.Application.Interfaces.Repositories;
using NZ.Payroll.Infrastructure.Persistence;

namespace NZ.Payroll.Infrastructure.Repositories;

public class PayrollAdjustmentHistoryRepository : IPayrollAdjustmentHistoryRepository
{
    private const string HistoryTableSql = "\"payroll\".\"payroll_adjustment_history\"";
    private readonly PayrollDbContext _context;

    public PayrollAdjustmentHistoryRepository(PayrollDbContext context)
    {
        _context = context;
    }

    public async Task<PayPayrollAdjustmentHistory> AddAsync(PayPayrollAdjustmentHistory entity, CancellationToken cancellationToken = default)
    {
        await EnsureHistoryTableExistsAsync(cancellationToken);
        await _context.PayPayrollAdjustmentHistories.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    private async Task EnsureHistoryTableExistsAsync(CancellationToken cancellationToken)
    {
        var tableExists = await _context.Database
            .SqlQueryRaw<int>("select 1 where to_regclass('payroll.payroll_adjustment_history') is not null")
            .AnyAsync(cancellationToken);

        if (tableExists)
        {
            return;
        }

        await _context.Database.ExecuteSqlRawAsync($"""
            create table if not exists {HistoryTableSql} (
                "Id" CHAR(26) not null primary key,
                "PayrollAdjustmentId" CHAR(26) not null,
                "Action" text not null,
                "PerformedBy" text null,
                "PerformedOn" timestamp with time zone not null,
                "Notes" text null,
                "OldData" text null,
                "NewData" text null,
                "CreatedOn" timestamp with time zone not null default NOW(),
                "CreatedBy" text not null,
                "UpdatedOn" timestamp with time zone not null default NOW(),
                "UpdatedBy" text not null,
                "IsActive" boolean not null,
                "SortOrder" integer not null,
                constraint "FK_payroll_adjustment_history_payroll_adjustment_PayrollAdjustmentId"
                    foreign key ("PayrollAdjustmentId") references "payroll"."payroll_adjustment" ("Id") on delete cascade
            );

            create index if not exists "IX_payroll_adjustment_history_PayrollAdjustmentId"
                on {HistoryTableSql} ("PayrollAdjustmentId");
            """, cancellationToken);
    }
}
