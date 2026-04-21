using ElektronickePosudky.Domain.Entities;

namespace ElektronickePosudky.Application.Repositories;

public interface ICiselnikRepository
{
    Task<List<Ciselnik>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<CiselnikPolozka>> GetItemsByCodeAsync(string kod, CancellationToken cancellationToken = default);
    Task<bool> PolozkaExistsAsync(string ciselnikKod, string polozkaKod, CancellationToken cancellationToken);
}