using ElektronickePosudky.Application.Interfaces;
using ElektronickePosudky.Domain.Entities.PosudekAggregate;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ElektronickePosudky.Infrastructure.Services;

public class QuestPdfExportService : IPdfExportService
{
    public byte[] GeneratePosudekPdf(PosudekRo posudek)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header().Element(ComposeHeader);

                page.Content().Element(x => ComposeContent(x, posudek));

                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Stránka ");
                    x.CurrentPageNumber();
                    x.Span(" z ");
                    x.TotalPages();
                });
            });
        });

        return document.GeneratePdf();
    }

    private void ComposeHeader(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Text("LÉKAŘSKÝ POSUDEK (Medical Certificate)").FontSize(20).SemiBold().FontColor(Colors.Blue.Darken2);
                column.Item().Text("o zdravotní způsobilosti k řízení motorových vozidel (Fitness for Driving Motor Vehicles)").FontSize(14);
            });
        });
    }

    private void ComposeContent(IContainer container, PosudekRo posudek)
    {
        container.PaddingVertical(1, Unit.Centimetre).Column(column =>
        {
            column.Spacing(10);

            column.Item().Background(Colors.Grey.Lighten3).Padding(5).Text("1. Údaje o pacientovi").SemiBold();
            column.Item().Text($"Jméno a příjmení (First name and last name): {posudek.Hlavicka.Pacient.Jmeno} {posudek.Hlavicka.Pacient.Prijmeni}");
            column.Item().Text($"Datum narození (Date of birth): {posudek.Hlavicka.Pacient.DatumNarozeni:dd.MM.yyyy}");
            column.Item().Text($"Adresa trvalého pobytu (Address of permanent residence): {posudek.Hlavicka.Pacient.Adresa}");

            column.Item().PaddingTop(15).Background(Colors.Grey.Lighten3).Padding(5).Text("2. Závěr").SemiBold();
            column.Item().Text($"Stav posudku (Status): {posudek.Hlavicka.StavPosudku.CiselnikKod}");
            column.Item().Text($"Platnost do (Valid until): {posudek.Hlavicka.PlatnostDo:dd.MM.yyyy}");
        });
    }
}