using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using DNDA.Web.Models.Reports;

public class BuyerActivityReportDocument : IDocument
{
    private readonly List<BuyerActivity> _buyers;
    private readonly DateTime _dateFrom;
    private readonly DateTime _dateTo;

    public BuyerActivityReportDocument(List<BuyerActivity> buyers, DateTime dateFrom, DateTime dateTo)
    {
        _buyers = buyers;
        _dateFrom = dateFrom;
        _dateTo = dateTo;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(40);
            page.DefaultTextStyle(x => x.FontSize(11));

            // HEADER
            page.Header().Column(header =>
            {
                header.Item()
                    .Text("Reporte de Actividad de Compradores")
                    .FontSize(20)
                    .Bold()
                    .FontColor(Colors.Blue.Darken2);

                header.Item()
                    .Text($"Período: {_dateFrom:dd/MM/yyyy} — {_dateTo:dd/MM/yyyy}")
                    .FontSize(11)
                    .FontColor(Colors.Grey.Darken1);

                header.Item().PaddingTop(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten1);
            });

            // CONTENT
            page.Content().PaddingTop(20).Column(col =>
            {
                // Encabezados de la tabla
                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(3); // Nombre
           
                        columns.RelativeColumn(2); // Subastas
                        columns.RelativeColumn(2); // Ofertas
                    });

                    // Fila de encabezados
                    table.Header(header =>
                    {
                        header.Cell().Background(Colors.Blue.Darken2).Padding(6)
                            .Text("Comprador").Bold().FontColor(Colors.White);
                        header.Cell().Background(Colors.Blue.Darken2).Padding(6)
                          
                            .Text("Subastas participadas").Bold().FontColor(Colors.White);
                        header.Cell().Background(Colors.Blue.Darken2).Padding(6)
                            .Text("Total de Pujas").Bold().FontColor(Colors.White);
                    });

                    // Filas de datos
                    foreach (var (buyer, index) in _buyers.Select((b, i) => (b, i)))
                    {
                        var bgColor = index % 2 == 0 ? Colors.White : Colors.Grey.Lighten4;

                        table.Cell().Background(bgColor).Padding(6).Text(buyer.BuyerName);
                
                        table.Cell().Background(bgColor).Padding(6).Text(buyer.AuctionsParticipated.ToString());
                        table.Cell().Background(bgColor).Padding(6).Text(buyer.TotalBids.ToString());
                    }
                });

                // Total al final
                col.Item().PaddingTop(15)
                    .Text($"Total de compradores: {_buyers.Count}")
                    .Bold()
                    .FontSize(11);
            });

            // FOOTER
            page.Footer()
                .AlignCenter()
                .Text(text =>
                {
                    text.Span("Página ").FontSize(9).FontColor(Colors.Grey.Darken1);
                    text.CurrentPageNumber().FontSize(9).FontColor(Colors.Grey.Darken1);
                    text.Span(" de ").FontSize(9).FontColor(Colors.Grey.Darken1);
                    text.TotalPages().FontSize(9).FontColor(Colors.Grey.Darken1);
                });
        });
    }
}