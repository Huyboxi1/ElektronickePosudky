using AutoMapper;
using System.Text.Json;
using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Domain.Entities;
using ElektronickePosudky.Domain.Entities.PosudekAggregate;
using ElektronickePosudky.Domain.ValueObjects;
using PosudekRo = ElektronickePosudky.Domain.Entities.PosudekAggregate.PosudekRo;

namespace ElektronickePosudky.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CodebookItemDto, CodebookReference>();
        CreateMap<CodebookReference, CodebookItemDto>();

        CreateMap<Ciselnik, CiselnikDto>()
            .ForMember(dest => dest.Preklady, opt => opt.MapFrom(src =>
                DeserializeTranslations(src.PrekladyJson)
            ))
            .ForMember(dest => dest.Termx, opt => opt.MapFrom(_ => false))
            .ForMember(dest => dest.TermxId, opt => opt.MapFrom(_ => (string?)null))
            .ForMember(dest => dest.TermxUrl, opt => opt.MapFrom(_ => (string?)null));


        CreateMap<CiselnikPolozka, CiselnikPolozkaDto>()
            .ForMember(dest => dest.Preklady, opt => opt.MapFrom(src =>
                DeserializeTranslations(src.PrekladyJson)
            ));


        CreateMap<PosudekHistorie, PosudekHistorieDto>()
            .ForMember(dest => dest.TypOperace, opt => opt.MapFrom(src => src.TypOperace))
            .ForMember(dest => dest.DatumOperace, opt => opt.MapFrom(src => src.DatumOperace))
            .ForMember(dest => dest.Lekar, opt => opt.MapFrom(src => src.Lekar))
            .ForMember(dest => dest.Poskytovatel, opt => opt.MapFrom(src => src.Poskytovatel));


        CreateMap<CiselnikPolozkaReference, CiselnikPolozkaReferenceDto>()
            .ForMember(dest => dest.Preklady, opt => opt.MapFrom(src =>
                (src.Preklady ?? new Dictionary<string, TranslationVO>()).ToDictionary(
                    kvp => kvp.Key,
                    kvp => new TranslationItemDto { Nazev = kvp.Value.Nazev, Popis = kvp.Value.Popis }
                )
            ));

        CreateMap<CiselnikPolozkaReferenceDto, CiselnikPolozkaReference>()
            .ForMember(dest => dest.Preklady, opt => opt.MapFrom(src =>
                (src.Preklady ?? new Dictionary<string, TranslationItemDto>()).ToDictionary(
                    kvp => kvp.Key,
                    kvp => new TranslationVO(kvp.Value.Nazev, kvp.Value.Popis)
                )
            ));

        CreateMap<PacientVO, PacientDetailDto>();
        CreateMap<PacientDetailDto, PacientVO>();

        CreateMap<ZdravotnickyPracovnikVO, ZdravotnickyPracovnikDetailDto>();
        CreateMap<ZdravotnickyPracovnikDetailDto, ZdravotnickyPracovnikVO>();

        CreateMap<PoskytovatelVO, PoskytovatelDetailDto>();
        CreateMap<PoskytovatelDetailDto, PoskytovatelVO>();

        CreateMap<PosudekSkupinaRo, SkupinaRidicskehoOpravneniDetailDto>()
            .ForMember(dest => dest.SkupinaRo, opt => opt.MapFrom(src => src.SkupinaRo));

        CreateMap<PosudekHarmonizovanyKod, HarmonizovanyKodDetailResponseDto>()
            .ForMember(dest => dest.HarmonizovanyKod, opt => opt.MapFrom(src => src.HarmonizovanyKod))
            .ForMember(dest => dest.SkupinaRo, opt => opt.MapFrom(src => src.SkupinaRo))
            .ForMember(dest => dest.UpresneniKod, opt => opt.MapFrom(src => src.UpresneniKod));

        CreateMap<PosudekNarodniKod, NarodniKodDetailResponseDto>()
            .ForMember(dest => dest.NarodniKod, opt => opt.MapFrom(src => src.NarodniKod))
            .ForMember(dest => dest.SkupinaRo, opt => opt.MapFrom(src => src.SkupinaRo));

        CreateMap<PosudekHlavicka, PosudekHlavickaResponseDto>()
            .ForMember(dest => dest.Pacient, opt => opt.MapFrom(src => src.Pacient))
            .ForMember(dest => dest.ZdravotnickyPracovnik, opt => opt.MapFrom(src => src.ZdravotnickyPracovnik))
            .ForMember(dest => dest.PoskytovatelZdravotnickychSluzeb, opt => opt.MapFrom(src => src.PoskytovatelZdravotnickychSluzeb))
            .ForMember(dest => dest.OdbornostLekare, opt => opt.MapFrom(src => src.OdbornostLekare))
            .ForMember(dest => dest.StavPosudku, opt => opt.MapFrom(src => src.StavPosudku))
            .ForMember(dest => dest.DruhProhlidky, opt => opt.MapFrom(src => src.DruhProhlidky))
            .ForMember(dest => dest.DruhPosudku, opt => opt.MapFrom(src => src.DruhPosudku))
            .ForMember(dest => dest.DatumVystaveni, opt => opt.MapFrom(src => src.DatumVystaveni))
            .ForMember(dest => dest.PlatnostDo, opt => opt.MapFrom(src => src.PlatnostDo))
            .ForMember(dest => dest.DatumVytvoreni, opt => opt.MapFrom(src => src.DatumVytvoreni));

        CreateMap<PosudekZpusobilost, PosudekZpusobilostResponseDto>()
            .ForMember(dest => dest.SkupinaZadateleRidic, opt => opt.MapFrom(src => src.SkupinaZadateleRidic))
            .ForMember(dest => dest.SkupinyRidicskehoOpravneni, opt => opt.MapFrom(src => src.SkupinyRidicskehoOpravneni))
            .ForMember(dest => dest.Vysledek, opt => opt.MapFrom(src => src.Vysledek))
            .ForMember(dest => dest.HarmonizovaneKody, opt => opt.MapFrom(src => src.HarmonizovaneKody))
            .ForMember(dest => dest.NarodniKody, opt => opt.MapFrom(src => src.NarodniKody))
            .ForMember(dest => dest.VerzeZaznamu, opt => opt.MapFrom(src => src.VerzeZaznamu));

        CreateMap<PosudekRo, PosudekRoDetailDto>()
            .ForMember(dest => dest.Pacient, opt => opt.MapFrom(src => src.Hlavicka.Pacient))
            .ForMember(dest => dest.ZdravotnickyPracovnik, opt => opt.MapFrom(src => src.Hlavicka.ZdravotnickyPracovnik))
            .ForMember(dest => dest.PoskytovatelZdravotnickychSluzeb, opt => opt.MapFrom(src => src.Hlavicka.PoskytovatelZdravotnickychSluzeb))
            .ForMember(dest => dest.OdbornostLekare, opt => opt.MapFrom(src => src.Hlavicka.OdbornostLekare))
            .ForMember(dest => dest.StavPosudku, opt => opt.MapFrom(src => src.Hlavicka.StavPosudku))
            .ForMember(dest => dest.DruhProhlidky, opt => opt.MapFrom(src => src.Hlavicka.DruhProhlidky))
            .ForMember(dest => dest.DruhPosudku, opt => opt.MapFrom(src => src.Hlavicka.DruhPosudku))
            .ForMember(dest => dest.DatumVystaveni, opt => opt.MapFrom(src => src.Hlavicka.DatumVystaveni))
            .ForMember(dest => dest.PlatnostDo, opt => opt.MapFrom(src => src.Hlavicka.PlatnostDo))
            .ForMember(dest => dest.DatumVytvoreni, opt => opt.MapFrom(src => src.Hlavicka.DatumVytvoreni))
            .ForMember(dest => dest.VerzeZaznamu, opt => opt.MapFrom(src => src.Hlavicka.VerzeZaznamu))
            .ForMember(dest => dest.Vysledek, opt => opt.MapFrom(src =>
                src.Zpusobilosti.FirstOrDefault() != null ? src.Zpusobilosti.FirstOrDefault()!.Vysledek : null!))
            .ForMember(dest => dest.SkupinaZadatelRidic, opt => opt.MapFrom(src =>
                src.Zpusobilosti.FirstOrDefault() != null ? src.Zpusobilosti.FirstOrDefault()!.SkupinaZadateleRidic : null!))
            .ForMember(dest => dest.SkupinyRidicskehoOpravneni, opt => opt.MapFrom(src =>
                src.Zpusobilosti.FirstOrDefault() != null ? src.Zpusobilosti.FirstOrDefault()!.SkupinyRidicskehoOpravneni : new List<PosudekSkupinaRo>()))
            .ForMember(dest => dest.HarmonizovaneKody, opt => opt.MapFrom(src =>
                src.Zpusobilosti.FirstOrDefault() != null ? src.Zpusobilosti.FirstOrDefault()!.HarmonizovaneKody : new List<PosudekHarmonizovanyKod>()))
            .ForMember(dest => dest.NarodniKody, opt => opt.MapFrom(src =>
                src.Zpusobilosti.FirstOrDefault() != null ? src.Zpusobilosti.FirstOrDefault()!.NarodniKody : new List<PosudekNarodniKod>()));
    }

    private static Dictionary<string, TranslationItemDto> DeserializeTranslations(string? prekladyJson)
    {
        if (string.IsNullOrWhiteSpace(prekladyJson))
        {
            return new Dictionary<string, TranslationItemDto>();
        }

        try
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var translations = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(prekladyJson, options);

            if (translations == null)
            {
                return new Dictionary<string, TranslationItemDto>();
            }

            var result = new Dictionary<string, TranslationItemDto>();
            foreach (var kvp in translations)
            {
                if (kvp.Value.TryGetProperty("nazev", out var nazev) &&
                    kvp.Value.TryGetProperty("popis", out var popis))
                {
                    result[kvp.Key] = new TranslationItemDto
                    {
                        Nazev = nazev.GetString() ?? string.Empty,
                        Popis = popis.GetString() ?? string.Empty
                    };
                }
            }

            return result;
        }
        catch
        {
            return [];
        }
    }
}