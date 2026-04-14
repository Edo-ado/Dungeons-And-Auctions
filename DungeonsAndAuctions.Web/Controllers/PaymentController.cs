using D_A.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace DNDA.Web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IServicePayment _service;
        private readonly IServiceUser _serviceUser;
        private readonly IServiceAuctions _serviceSubasta;
        private readonly IServiceObject _serviceObject;





        public PaymentController(IServicePayment service, IServiceUser serviceUser, IServiceAuctions serviceSubasta, IServiceObject serviceObject)
        {
            _service = service;
            _serviceUser = serviceUser;
            _serviceSubasta = serviceSubasta;
            _serviceObject = serviceObject;
        }

        //ver el proceso de pago de una subasta
        public async Task<IActionResult> Details(int id)
        {
            var payment = await _service.GetPaymentByAuctionAsync(id);
            if (payment == null)
                return NotFound("payment es null");


            var usercomprador = await _serviceUser.GetWinnerUserByPaymentAsync(payment.WinnerUserId.Value);

            var auction = await _serviceSubasta.GetAuctionById(payment.AuctionId.Value);

            var objetoA = await _serviceObject.FindByIdAsync(auction.idobject.Value);

            ViewBag.NameObject = objetoA.Name;


        

            ViewBag.UsuarioNombre = usercomprador.FirstName + " " + usercomprador.LastName;
            ViewBag.UserTag = usercomprador.UserName;
            return View(payment);
        }

        //confirmación del pago 
        [HttpPost]
        public async Task<IActionResult> Confirm(int paymentId)
        {
            await _service.ConfirmPaymentAsync(paymentId);
            return RedirectToAction("Details", new { paymentId });
        }
    }
}
