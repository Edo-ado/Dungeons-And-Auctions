using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using D_A.Application.DTOs;
using DNDA.Web.Models.Reports;

public class SystemActivityReportDocument : IDocument
{
    private readonly SystemActivity _data;

    public SystemActivityReportDocument(SystemActivity data)
    {
        _data = data;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(40);
            page.DefaultTextStyle(x => x.FontSize(11));

            page.Header().Column(header =>
            {
                header.Item()
                    .Text("Reporte de Actividad del Sistema")
                    .FontSize(20).Bold().FontColor(Colors.Blue.Darken2);

                header.Item()
                    .Text($"Período: {_data.DateFrom:dd/MM/yyyy} — {_data.DateTo:dd/MM/yyyy}")
                    .FontSize(11).FontColor(Colors.Grey.Darken1);

                header.Item().PaddingTop(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten1);
            });

            page.Content().PaddingTop(20).Column(col =>
            {
                // Tarjetas de métricas
                col.Item().Row(row =>
                {
                    row.RelativeItem().Border(1).BorderColor(Colors.Grey.Lighten2)
                        .Padding(16).Column(c =>
                        {
                            c.Item().Text("Subastas Creadas").FontSize(10)
                                .FontColor(Colors.Grey.Darken1);
                            c.Item().Text(_data.AuctionsCreated.ToString())
                                .FontSize(32).Bold().FontColor(Colors.Blue.Darken2);
                        });

                    row.ConstantItem(10); // espacio

                    row.RelativeItem().Border(1).BorderColor(Colors.Grey.Lighten2)
                        .Padding(16).Column(c =>
                        {
                            c.Item().Text("Pujas Realizadas").FontSize(10)
                                .FontColor(Colors.Grey.Darken1);
                            c.Item().Text(_data.BidsPlaced.ToString())
                                .FontSize(32).Bold().FontColor(Colors.Red.Darken2);
                        });

                    row.ConstantItem(10);

                    row.RelativeItem().Border(1).BorderColor(Colors.Grey.Lighten2)
                        .Padding(16).Column(c =>
                        {
                            c.Item().Text("Subastas Finalizadas").FontSize(10)
                                .FontColor(Colors.Grey.Darken1);
                            c.Item().Text(_data.AuctionsFinished.ToString())
                                .FontSize(32).Bold().FontColor(Colors.Grey.Darken2);
                        });
                });

                col.Item().PaddingTop(30)
                    .Text("Resumen del período")
                    .FontSize(13).Bold();

                col.Item().PaddingTop(10).Text(
                    $"Durante el período del {_data.DateFrom:dd/MM/yyyy} al {_data.DateTo:dd/MM/yyyy}, " +
                    $"el sistema registró {_data.AuctionsCreated} subastas creadas, " +
                    $"{_data.BidsPlaced} pujas realizadas y " +
                    $"{_data.AuctionsFinished} subastas finalizadas."
                ).FontColor(Colors.Grey.Darken2);
            });

            page.Footer().AlignCenter().Text(text =>
            {
                text.Span("Página ").FontSize(9).FontColor(Colors.Grey.Darken1);
                text.CurrentPageNumber().FontSize(9).FontColor(Colors.Grey.Darken1);
                text.Span(" de ").FontSize(9).FontColor(Colors.Grey.Darken1);
                text.TotalPages().FontSize(9).FontColor(Colors.Grey.Darken1);
            });
        });
    }
}