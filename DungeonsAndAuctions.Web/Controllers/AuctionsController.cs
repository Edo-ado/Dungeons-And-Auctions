using System.Diagnostics;
using System.Threading.Tasks;
using D_A.Application.DTOs;
using D_A.Application.Services.Implementations;
using D_A.Application.Services.Interfaces;
using D_A.Infraestructure.Models;
using D_A.Web.Models;
using DNDA.Web.Util;
using Microsoft.AspNetCore.Mvc;


namespace DNDA.Web.Controllers


{
    public class AuctionsController : Controller
    {


        private readonly IServiceAuctions _ServiceAuctions;
        private readonly IServiceObject _ServiceObject;
        private readonly IServiceUser _serviceUser;
        private readonly IServiceAuctionBidHistory _serviceBidHistory;

        public AuctionsController(IServiceAuctions ServiceAuctions, IServiceObject ServiceObject, IServiceUser serviceUser, IServiceAuctionBidHistory serviceBidHistory)
        {
            _ServiceAuctions = ServiceAuctions;
            _serviceUser = serviceUser;
            _ServiceObject = ServiceObject;
            _serviceBidHistory = serviceBidHistory;


        }


        public async Task<IActionResult> Index()
        {

            var all = await _ServiceAuctions.GetAllAuctions();
            var active = await _ServiceAuctions.GetAllAuctionsActive();
            var closed = await _ServiceAuctions.GetAllAuctionsClosed();
            var banned = await _ServiceAuctions.GetAllAuctionsBanned();
            var Inactive = await _ServiceAuctions.GetAllAuctionsInactive();



            ViewBag.Open = active;
            ViewBag.Close = closed;
            ViewBag.Banned = banned;
            ViewBag.Inactive = Inactive;





            return View(all);


        }

        public async Task<IActionResult> IndexMaintenance()
        {
            var all = await _ServiceAuctions.GetAllAuctions();

            return View(all);
        }


        public async Task<IActionResult> Details(int id)
        {
            var auction = await _ServiceAuctions.AllDetails(id);


            return View(auction);

        }

        public int CantidadDePujas(int id)
        {
            return _serviceBidHistory.CountBidsByAuction(id).Result;
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Objects = await _ServiceObject.ListActiveAsync();
            var userAsigned = await _serviceUser.FindByIdAsync(2);//usuario simulado
            ViewBag.UserName = userAsigned?.UserName;
            ViewBag.UserId = userAsigned?.Id;



            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(AuctionsDTO auction)
        {

            ModelState.Remove("idusercreator");
            ModelState.Remove("IdusercreatorNavigation");
            ModelState.Remove("IduserNavigation");
            ModelState.Remove("IdstateNavigation");
            ModelState.Remove("IdobjectNavigation");
            ModelState.Remove("AuctionBidHistory");
            ModelState.Remove("BidHistory");
            ModelState.Remove("Images");


            ModelState.Remove("idobject");


            //validaciones
            if (auction.idobject == null || auction.idobject == 0)
            {
                ModelState.AddModelError("Idobject", "Debe seleccionar una reliquia.");

            }
            //fecha de cierre mayor a fecha inicio
            if (auction.EndDate <= auction.StartDate)
                ModelState.AddModelError("EndDate", "La fecha de cierre debe ser mayor a la fecha de inicio.");

            if (auction.StartDate == DateOnly.MinValue)
                ModelState.AddModelError("StartDate", "La fecha de inicio es requerida.");

            if (auction.EndDate == DateOnly.MinValue)
                ModelState.AddModelError("EndDate", "La fecha de cierre es requerida.");



            if (ModelState.IsValid)
            {

                //ver si el objeto está en subastas activas
                var HasActiveAuctions = await _ServiceObject.HasActiveAuctionAsync(auction.idobject!.Value);
                if (HasActiveAuctions)
                {
                    ModelState.AddModelError("idobject", "El objeto ya tiene una subasta activa.");


                    //debo de cargar todo de nuevo
                    ViewBag.Objects = await _ServiceObject.ListActiveAsync();

                    var userAsigned2 = await _serviceUser.FindByIdAsync(2);
                    ViewBag.UserName = userAsigned2?.UserName;
                    ViewBag.UserId = userAsigned2?.Id;

                    return View(auction);
                }

                //




                auction.idusercreator = 2;
                await _ServiceAuctions.CreateAuction(auction);

                return RedirectToAction(nameof(Index));
            }


            //lista de objetos
            ViewBag.Objects = await _ServiceObject.ListActiveAsync();


            //usuario asignado a crear subasta
            var userAsigned = await _serviceUser.FindByIdAsync(2);

            ViewBag.UserName = userAsigned?.UserName;

            ViewBag.UserId = userAsigned?.Id;

            return View(auction);
        }

   



       public async Task<ActionResult> Edit(int id)
        {
            var auction = await _ServiceAuctions.GetAuctionById(id);
            if (auction == null) return NotFound();

            if (auction.TotalBids > 0)
            {
                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                    "No permitido",
                    "No se puede editar una subasta que ya tiene pujas.",
                    SweetAlertMessageType.warning
                );
                return RedirectToAction(nameof(IndexMaintenance));
            }

            //notifico si la subasta está activa y no puedo editar
            if (auction.idstate == 1)
            {
                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                    "No permitido",
                    "No se puede editar una subasta que ya está abierta.",
                    SweetAlertMessageType.warning
                );
                return RedirectToAction(nameof(IndexMaintenance));
            }


            //cargo lo que necesito
            ViewBag.Objects = await _ServiceObject.ListActiveAsync();
            var user = await _serviceUser.FindByIdAsync(2);
            ViewBag.UserName = user?.UserName;
            ViewBag.UserId = user?.Id;

            return View(auction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, AuctionsDTO auction)
        {
            ModelState.Remove("idusercreator");
            ModelState.Remove("IdusercreatorNavigation");
            ModelState.Remove("IduserNavigation");
            ModelState.Remove("IdstateNavigation");
            ModelState.Remove("IdobjectNavigation");
            ModelState.Remove("AuctionBidHistory");
            ModelState.Remove("BidHistory");
            ModelState.Remove("Images");
            ModelState.Remove("idobject");

            if (auction.idobject == null || auction.idobject == 0)
                ModelState.AddModelError("idobject", "Debe seleccionar una reliquia.");

            if (auction.StartDate == DateOnly.MinValue)
                ModelState.AddModelError("StartDate", "La fecha de inicio es requerida.");

            if (auction.EndDate == DateOnly.MinValue)
                ModelState.AddModelError("EndDate", "La fecha de cierre es requerida.");

            if (auction.EndDate <= auction.StartDate)
                ModelState.AddModelError("EndDate", "La fecha de cierre debe ser mayor a la fecha de inicio.");

            if (!ModelState.IsValid)
            {
                var auctiontodo = await _ServiceAuctions.GetAuctionById(id);


                auction.IdobjectNavigation = auctiontodo?.IdobjectNavigation;
                auction.IdstateNavigation = auctiontodo?.IdstateNavigation;

                var usuario = await _serviceUser.FindByIdAsync(2);
                ViewBag.UserName = usuario?.UserName;
                ViewBag.UserId = usuario?.Id;
                return View(auction);
            }

            auction.Id = id;
            await _ServiceAuctions.UpdateAuction(auction);
            TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                "Subasta actualizada",
                "La subasta fue modificada exitosamente.",
                SweetAlertMessageType.success
            );
            return RedirectToAction(nameof(IndexMaintenance));


        }



    }
}