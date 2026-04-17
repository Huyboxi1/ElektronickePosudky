using ElektronickePosudky.Domain.Entities;
using ElektronickePosudky.Domain.Entities.PosudekAggregate;
using Microsoft.EntityFrameworkCore;

namespace ElektronickePosudky.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<PosudekRo> Posudky => Set<PosudekRo>();
    public DbSet<Ciselnik> Ciselniky => Set<Ciselnik>();
    public DbSet<CiselnikPolozka> CiselnikPolozky => Set<CiselnikPolozka>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}