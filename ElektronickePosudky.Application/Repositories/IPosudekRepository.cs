using ElektronickePosudky.Domain.Entities.PosudekAggregate;

namespace ElektronickePosudky.Application.Repositories;

public interface ITransaction : IAsyncDisposable
{
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
}

public interface IPosudekRepository
{
    Task<PosudekRo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(PosudekRo posudek, CancellationToken cancellationToken = default);
    void Update(PosudekRo posudek);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<(List<PosudekRo> Items, int TotalCount)> SearchAsync(
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
        CancellationToken cancellationToken = default);
    Task<bool> VerifyAuthorizationAsync(string krzpId, string ico, CancellationToken cancellationToken = default);
    Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}