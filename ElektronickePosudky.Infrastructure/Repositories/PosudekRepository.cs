using ElektronickePosudky.Application.Repositories;
using ElektronickePosudky.Domain.Entities;
using ElektronickePosudky.Domain.Entities.PosudekAggregate;
using ElektronickePosudky.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PosudekRo = ElektronickePosudky.Domain.Entities.PosudekAggregate.PosudekRo;

namespace ElektronickePosudky.Infrastructure.Repositories;

public class DbContextTransaction : ITransaction
{
    private readonly IDbContextTransaction _transaction;

    public DbContextTransaction(IDbContextTransaction transaction)
    {
        _transaction = transaction;
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _transaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        await _transaction.RollbackAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        await _transaction.DisposeAsync();
    }
}

public class PosudekRepository : IPosudekRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PosudekRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PosudekRo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Posudky
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(PosudekRo posudek, CancellationToken cancellationToken = default)
    {
        await _dbContext.Posudky.AddAsync(posudek, cancellationToken);
    }

    public void Update(PosudekRo posudek)
    {
        _dbContext.Posudky.Update(posudek);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<(List<PosudekRo> Items, int TotalCount)> SearchAsync(
        string? rid,
        DateTime? datumOd,
        DateTime? datumDo,
        bool? jenPlatne,
        Guid? stavPosudku,
        string? fulltext,
        string? ico,
        int page,
        int size,
        string? sort,
        string? order,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Posudky.AsNoTracking();

        // RID filter
        if (!string.IsNullOrWhiteSpace(rid))
        {
            query = query.Where(x => x.Hlavicka.Pacient.Rid == rid);
        }

        // Date range filters
        if (datumOd.HasValue)
        {
            query = query.Where(x => x.Hlavicka.DatumVystaveni >= datumOd.Value);
        }

        if (datumDo.HasValue)
        {
            query = query.Where(x => x.Hlavicka.DatumVystaveni <= datumDo.Value);
        }

        // Only valid opinions filter
        if (jenPlatne.HasValue && jenPlatne.Value)
        {
            query = query.Where(x => x.Hlavicka.StavPosudku.PolozkaKod != "ZNEPLATNENY");
        }

        // Opinion state filter
        if (stavPosudku.HasValue)
        {
            // Assuming stavPosudku is the ID or code of the state
            query = query.Where(x => x.Hlavicka.StavPosudku.PolozkaKod == stavPosudku.ToString());
        }

        // Fulltext search
        if (!string.IsNullOrWhiteSpace(fulltext))
        {
            query = query.Where(x =>
                (x.Hlavicka.Pacient.Jmeno ?? "").Contains(fulltext) ||
                (x.Hlavicka.Pacient.Prijmeni ?? "").Contains(fulltext) ||
                (x.Hlavicka.Pacient.Rid ?? "").Contains(fulltext));
        }

        // ICO filter (healthcare provider)
        if (!string.IsNullOrWhiteSpace(ico))
        {
            query = query.Where(x => x.Hlavicka.PoskytovatelZdravotnickychSluzeb.Ico == ico);
        }

        int totalCount = await query.CountAsync(cancellationToken);

        // Default sorting
        var sortColumn = sort?.ToLower() ?? "datumvystaveni";
        var isDescending = order?.ToLower() == "desc";

        query = sortColumn switch
        {
            "rid" => isDescending ? query.OrderByDescending(x => x.Hlavicka.Pacient.Rid) : query.OrderBy(x => x.Hlavicka.Pacient.Rid),
            "datumpacient" => isDescending ? query.OrderByDescending(x => x.Hlavicka.Pacient.DatumNarozeni) : query.OrderBy(x => x.Hlavicka.Pacient.DatumNarozeni),
            "datumvystaveni" => isDescending ? query.OrderByDescending(x => x.Hlavicka.DatumVystaveni) : query.OrderBy(x => x.Hlavicka.DatumVystaveni),
            _ => query.OrderByDescending(x => x.Hlavicka.DatumVystaveni)
        };

        var items = await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task<bool> VerifyAuthorizationAsync(string krzpId, string ico, CancellationToken cancellationToken = default)
    {
        var isAuthorized = await _dbContext.Posudky
            .AsNoTracking()
            .Where(x => x.Hlavicka.ZdravotnickyPracovnik.KrzpId == krzpId && x.Hlavicka.PoskytovatelZdravotnickychSluzeb.Ico == ico)
            .AnyAsync(cancellationToken);

        return isAuthorized;
    }

    public async Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        return new DbContextTransaction(transaction);
    }
}