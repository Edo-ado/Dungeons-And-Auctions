using D_A.Application.Services.Interfaces;
using DNDA.Web.Models.Reports;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


namespace DNDA.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly IServicePayment _service;
        private readonly IServiceUser _serviceUser;
        private readonly IServiceAuctions _serviceSubasta;
        private readonly IServiceObject _serviceObject;

        public ReportController(
            IServicePayment service,
            IServiceUser serviceUser,
            IServiceAuctions serviceSubasta,
            IServiceObject serviceObject)
        {
            _service = service;
            _serviceUser = serviceUser;
            _serviceSubasta = serviceSubasta;
            _serviceObject = serviceObject;
        }

        public IActionResult Index()
        {
            return View();
        }



        public async Task<IActionResult> GetBuyerActivityReport([FromQuery] DateTime dateFrom,
    [FromQuery] DateTime dateTo)
        {
            if (dateFrom > dateTo)
                return BadRequest("La fecha de inicio no puede ser mayor a la fecha final.");

            var data = await _serviceUser.GetBuyerActivityReportAsync(dateFrom, dateTo);

            if (data == null || !data.Any())
                return NotFound("No hay datos para el rango de fechas seleccionado.");

            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            var reportData = data.Select(x => new BuyerActivity
            {
                BuyerName = x.BuyerName,
                AuctionsParticipated = x.AuctionsParticipated,
                TotalBids = x.TotalBids,
                Email = ""
            }).ToList();

            var report = new BuyerActivityReportDocument(reportData, dateFrom, dateTo);
            var pdf = report.GeneratePdf();

            return File(
                pdf,
                "application/pdf",
                $"reporte-compradores-{dateFrom:yyyyMMdd}-{dateTo:yyyyMMdd}.pdf");
        }
    

        public async Task<IActionResult> GetSystemActivityReport(
    [FromQuery] DateTime dateFrom,
    [FromQuery] DateTime dateTo)
        {
            if (dateFrom > dateTo)
                return BadRequest("La fecha de inicio no puede ser mayor a la fecha final.");

            var data = await _serviceSubasta.GetSystemActivityReportAsync(dateFrom, dateTo);

            if (data == null)
                return NotFound("No hay datos para el rango seleccionado.");

            return Json(data);
        }

        //PDF
        public async Task<IActionResult> GetSystemActivityReportPdf(
            [FromQuery] DateTime dateFrom,
            [FromQuery] DateTime dateTo)
        {
            if (dateFrom > dateTo)
                return BadRequest("La fecha de inicio no puede ser mayor a la fecha final.");

            var data = await _serviceSubasta.GetSystemActivityReportAsync(dateFrom, dateTo);

            if (data == null)
                return NotFound("No hay datos para el rango seleccionado.");

            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            var report = new SystemActivityReportDocument(data);
            var pdf = report.GeneratePdf();

            return File(
                pdf,
                "application/pdf",
                $"reporte-sistema-{dateFrom:yyyyMMdd}-{dateTo:yyyyMMdd}.pdf");
        }



    }
}