using ElektronickePosudky.Domain.Entities.PosudekAggregate;

namespace ElektronickePosudky.Application.Interfaces;

public interface IPdfExportService
{
    byte[] GeneratePosudekPdf(PosudekRo posudek);
}