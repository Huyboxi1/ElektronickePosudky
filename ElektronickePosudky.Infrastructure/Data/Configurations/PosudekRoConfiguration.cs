using ElektronickePosudky.Domain.Entities.PosudekAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElektronickePosudky.Infrastructure.Data.Configurations;

public class PosudekRoConfiguration : IEntityTypeConfiguration<PosudekRo>
{
    public void Configure(EntityTypeBuilder<PosudekRo> builder)
    {
        builder.ToTable("PosudkyRo");
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.Hlavicka, h =>
        {
            h.OwnsOne(x => x.Pacient);
            h.OwnsOne(x => x.ZdravotnickyPracovnik);
            h.OwnsOne(x => x.PoskytovatelZdravotnickychSluzeb);

            h.OwnsOne(x => x.OdbornostLekare, o => o.Ignore(c => c.Preklady));

            h.OwnsOne(x => x.TypAkce, t => t.Ignore(c => c.Preklady));

            h.OwnsOne(x => x.StavPosudku, s => s.Ignore(c => c.Preklady));
            h.OwnsOne(x => x.DruhProhlidky, d => d.Ignore(c => c.Preklady));
            h.OwnsOne(x => x.DruhPosudku, d => d.Ignore(c => c.Preklady));

            h.Property(x => x.VerzeZaznamu).IsConcurrencyToken();
        });

        builder.HasMany(x => x.Historie)
            .WithOne()
            .HasForeignKey(x => x.PosudekRoId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Navigation(x => x.Historie)
            .Metadata.SetField("_historie");
        builder.Navigation(x => x.Historie)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(x => x.Zpusobilosti)
            .WithOne()
            .HasForeignKey(x => x.PosudekRoId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Navigation(x => x.Zpusobilosti)
            .Metadata.SetField("_zpusobilosti");
        builder.Navigation(x => x.Zpusobilosti)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}

public class PosudekZpusobilostConfiguration : IEntityTypeConfiguration<PosudekZpusobilost>
{
    public void Configure(EntityTypeBuilder<PosudekZpusobilost> builder)
    {
        builder.ToTable("PosudkyRo_Zpusobilosti");
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.SkupinaZadateleRidic, s => s.Ignore(c => c.Preklady));
        builder.OwnsOne(x => x.Vysledek, v => v.Ignore(c => c.Preklady));

        builder.HasMany(x => x.SkupinyRidicskehoOpravneni)
            .WithOne()
            .HasForeignKey(x => x.PosudekZpusobilostId);
        builder.Navigation(x => x.SkupinyRidicskehoOpravneni).Metadata.SetField("_skupinyRidicskehoOpravneni");

        builder.HasMany(x => x.HarmonizovaneKody)
            .WithOne()
            .HasForeignKey(x => x.PosudekZpusobilostId);
        builder.Navigation(x => x.HarmonizovaneKody).Metadata.SetField("_harmonizovaneKody");

        builder.HasMany(x => x.NarodniKody)
            .WithOne()
            .HasForeignKey(x => x.PosudekZpusobilostId);
        builder.Navigation(x => x.NarodniKody).Metadata.SetField("_narodniKody");
    }
}

public class PosudekHarmonizovanyKodConfiguration : IEntityTypeConfiguration<PosudekHarmonizovanyKod>
{
    public void Configure(EntityTypeBuilder<PosudekHarmonizovanyKod> builder)
    {
        builder.ToTable("PosudkyRo_HarmonizovaneKody");
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.HarmonizovanyKod, h => h.Ignore(c => c.Preklady));
        builder.OwnsOne(x => x.UpresneniKod, u => u.Ignore(c => c.Preklady));
        builder.Property(x => x.UpresneniText).HasMaxLength(500);

        builder.OwnsMany(x => x.SkupinaRo, sr =>
        {
            sr.ToTable("PosudkyRo_HarmonizovaneKody_SkupinyRO_Items");
            sr.WithOwner().HasForeignKey("PosudekHarmonizovanyKodId");
            sr.Property<int>("Id").ValueGeneratedOnAdd();
            sr.HasKey("Id");
            sr.Ignore(c => c.Preklady);
        });

        builder.Navigation(x => x.SkupinaRo)
            .Metadata.SetField("_skupinaRo");
        builder.Navigation(x => x.SkupinaRo)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}

public class PosudekHistorieConfiguration : IEntityTypeConfiguration<PosudekHistorie>
{
    public void Configure(EntityTypeBuilder<PosudekHistorie> builder)
    {
        builder.ToTable("PosudkyRo_Historie");
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.TypOperace, t => t.Ignore(c => c.Preklady));
        builder.OwnsOne(x => x.Lekar);
        builder.OwnsOne(x => x.Poskytovatel);
    }
}

public class PosudekNarodniKodConfiguration : IEntityTypeConfiguration<PosudekNarodniKod>
{
    public void Configure(EntityTypeBuilder<PosudekNarodniKod> builder)
    {
        builder.ToTable("PosudkyRo_NarodniKody");
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.NarodniKod, n => n.Ignore(c => c.Preklady));
        builder.OwnsOne(x => x.SkupinaRo, s => s.Ignore(c => c.Preklady));
    }
}

public class PosudekSkupinaRoConfiguration : IEntityTypeConfiguration<PosudekSkupinaRo>
{
    public void Configure(EntityTypeBuilder<PosudekSkupinaRo> builder)
    {
        builder.ToTable("PosudkyRo_SkupinyRO");
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.SkupinaRo, sr => sr.Ignore(c => c.Preklady));
    }
}