using ElektronickePosudky.Application.Repositories;
using ElektronickePosudky.Domain.Entities;
using ElektronickePosudky.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ElektronickePosudky.Infrastructure.Repositories;

public class CiselnikRepository : ICiselnikRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CiselnikRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Ciselnik>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Ciselniky
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<CiselnikPolozka>> GetItemsByCodeAsync(string kod, CancellationToken cancellationToken = default)
    {
        return await _dbContext.CiselnikPolozky
            .AsNoTracking()
            .Where(x => x.Kod == kod)
            .ToListAsync(cancellationToken);
    }
}
