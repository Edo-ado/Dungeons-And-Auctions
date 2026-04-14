using System.Diagnostics;
using System.Threading.Tasks;
using D_A.Application.DTOs;
using D_A.Application.Services.Implementations;
using D_A.Application.Services.Interfaces;
using D_A.Infraestructure.Models;
using D_A.Web.Models;
using DNDA.Web.Hubs;
using DNDA.Web.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DNDA.Web.Controllers
{
    public class AuctionsController : Controller
    {
        private readonly IServiceAuctions _ServiceAuctions;
        private readonly IServiceObject _ServiceObject;
        private readonly IServiceUser _serviceUser;
        private readonly IServiceAuctionBidHistory _serviceBidHistory;
        private readonly IHubContext<AuctionHub> _hubContext;

        


        private const int UsuarioActualId = 2;
       

        public AuctionsController(
            IServiceAuctions ServiceAuctions,
            IServiceObject ServiceObject,
            IServiceUser serviceUser,
            IServiceAuctionBidHistory serviceBidHistory,
            IHubContext<AuctionHub> hubContext)
        {
            _ServiceAuctions = ServiceAuctions;
            _serviceUser = serviceUser;
            _ServiceObject = ServiceObject;
            _serviceBidHistory = serviceBidHistory;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> Index()
        {
            var all = await _ServiceAuctions.GetAllAuctionsValid();
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
            if (auction == null) return NotFound();

          
            var currentUser = await _serviceUser.FindByIdAsync(UsuarioActualId);
            ViewBag.CurrentUserId = UsuarioActualId;
            ViewBag.CurrentUserName = currentUser?.UserName ?? "Desconocido";

           
            ViewBag.IsOwner = auction.idusercreator == UsuarioActualId;

            return View(auction);
        }

      
        [HttpPost]
              [HttpPost]
        public async Task<IActionResult> PlaceBid(int auctionId, decimal amount)
        {
           
            int userId = UsuarioActualId;

            var errorMsg = await _serviceBidHistory.PlaceBidAsync(auctionId, userId, amount);

            if (errorMsg != null)
                return Json(new { success = false, message = errorMsg });

            try
            {
              
                var highestBid = await _serviceBidHistory.GetHighestBidAsync(auctionId);
                var bids = await _serviceBidHistory.GetBidsByAuctionAsync(auctionId);

             
                var historyPayload = bids.Select(b => new
                {
                    userName = b.User?.UserName ?? "—",
                    amount = b.Amount,
                    bidDate = b.BidDate.ToString("yyyy-MM-dd HH:mm:ss")
                }).ToList();

               
                await _hubContext.Clients.Group($"auction-{auctionId}").SendAsync(
                    "BidPlaced",
                    new
                    {
                        auctionId,
                        highestAmount = highestBid?.Amount ?? 0,
                        highestUser = highestBid?.User?.UserName ?? "—",
                        highestUserId = highestBid?.UserId ?? 0,
                        bidHistory = historyPayload
                    }
                );


                return Json(new
                {
                    success = true,
                    message = "¡Puja registrada con éxito!",
                    highestAmount = highestBid?.Amount ?? 0,
                    highestUser = highestBid?.User?.UserName ?? "—"
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SignalR Error] PlaceBid: {ex.Message}");
             
            }

            return Json(new { success = true, message = "Puja registrada con éxito" });


        }
     
        [HttpGet]
        public async Task<IActionResult> GetAuctionState(int id)
        {
            var auction = await _ServiceAuctions.GetAuctionById(id);
            if (auction == null) return NotFound();

            var highestBid = await _serviceBidHistory.GetHighestBidAsync(id);
            var bids = await _serviceBidHistory.GetBidsByAuctionAsync(id);

            return Json(new
            {
                idstate = auction.idstate,
                stateName = auction.IdstateNavigation?.Name ?? "",
                highestAmount = highestBid?.Amount ?? auction.BasePrice ?? 0,
                highestUser = highestBid?.User?.UserName ?? "Sin pujas",
                highestUserId = highestBid?.UserId ?? 0,
                bidHistory = bids.Select(b => new
                {
                    userName = b.User?.UserName ?? "—",
                    amount = b.Amount,
                    bidDate = b.BidDate.ToString("yyyy-MM-dd HH:mm:ss")
                })
            });
        }

       
        [HttpPost]
        public async Task<IActionResult> CheckAndCloseAuction(int id)
        {
            var auction = await _ServiceAuctions.GetAuctionById(id);
            if (auction == null) return Json(new { closed = false });

           
            if (auction.idstate != 1)
                return Json(new { closed = false, alreadyClosed = true });

         
            var today = DateOnly.FromDateTime(DateTime.Now);
            if (today < auction.EndDate)
                return Json(new { closed = false });

          
            await _ServiceAuctions.CloseAuction(id); 
            

            var winnerBid = await _serviceBidHistory.GetHighestBidAsync(id);
            string winnerName = winnerBid != null
                ? (winnerBid.User?.UserName ?? "—")
                : "Sin ganador";

          
            await _hubContext.Clients.Group($"auction-{id}").SendAsync(
                "AuctionClosed",
                new
                {
                    auctionId = id,
                    winnerName,
                    winnerUserId = winnerBid?.UserId ?? 0,
                    winningAmount = winnerBid?.Amount ?? 0,
                    hasWinner = winnerBid != null
                }
            );

            return Json(new
            {
                closed = true,
                winnerName,
                winningAmount = winnerBid?.Amount ?? 0,
                hasWinner = winnerBid != null
            });
        }

        public int CantidadDePujas(int id)
            => _serviceBidHistory.CountBidsByAuction(id).Result;

        

        public async Task<IActionResult> Create()
        {
            ViewBag.Objects = await _ServiceObject.ListActiveAsync();
            var userAsigned = await _serviceUser.FindByIdAsync(UsuarioActualId);
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

            if (auction.idobject == null || auction.idobject == 0)
                ModelState.AddModelError("Idobject", "Debe seleccionar una reliquia.");

            if (auction.EndDate <= auction.StartDate)
                ModelState.AddModelError("EndDate", "La fecha de cierre debe ser mayor a la fecha de inicio.");

            if (auction.StartDate == DateOnly.MinValue)
                ModelState.AddModelError("StartDate", "La fecha de inicio es requerida.");

            if (auction.EndDate == DateOnly.MinValue)
                ModelState.AddModelError("EndDate", "La fecha de cierre es requerida.");

            if (ModelState.IsValid)
            {
                var HasActiveAuctions = await _ServiceObject.HasActiveAuctionAsync(auction.idobject!.Value);

                if (HasActiveAuctions)
                {
                    ModelState.AddModelError("idobject", "El objeto ya tiene una subasta activa.");
                    ViewBag.Objects = await _ServiceObject.ListActiveAsync();
                    var userAsigned2 = await _serviceUser.FindByIdAsync(UsuarioActualId);
                    ViewBag.UserName = userAsigned2?.UserName;
                    ViewBag.UserId = userAsigned2?.Id;
                    return View(auction);
                }

                auction.idusercreator = UsuarioActualId;
                await _ServiceAuctions.CreateAuction(auction);

                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                    "Subasta creada",
                    "La subasta fue registrada exitosamente.",
                    SweetAlertMessageType.success
                );

                return RedirectToAction(nameof(IndexMaintenance));
            }

            ViewBag.Objects = await _ServiceObject.ListActiveAsync();
            var userAsigned = await _serviceUser.FindByIdAsync(UsuarioActualId);
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

            if (auction.idstate == 1)
            {
                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                    "No permitido",
                    "No se puede editar una subasta que ya está abierta.",
                    SweetAlertMessageType.warning
                );
                return RedirectToAction(nameof(IndexMaintenance));
            }

            ViewBag.Objects = await _ServiceObject.ListActiveAsync();
            var user = await _serviceUser.FindByIdAsync(UsuarioActualId);
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
                var auctionFull = await _ServiceAuctions.GetAuctionById(id);
                auction.IdobjectNavigation = auctionFull?.IdobjectNavigation;
                auction.IdstateNavigation = auctionFull?.IdstateNavigation;
                var usuario = await _serviceUser.FindByIdAsync(UsuarioActualId);
                ViewBag.UserName = usuario?.UserName;
                ViewBag.UserId = usuario?.Id;
                return View(auction);
            }

            //rama ash: asignar el ID antes de actualizar
            auction.Id = id;
            await _ServiceAuctions.UpdateAuction(auction);

            TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                "Subasta actualizada",
                "Los cambios fueron guardados correctamente.",
                SweetAlertMessageType.success
            );

            return RedirectToAction(nameof(IndexMaintenance));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PublishAuction(int id)
        {
            var auction = await _ServiceAuctions.GetAuctionById(id);
            if (auction == null)
            {
                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion("Error", "No se encontró la subasta indicada.", SweetAlertMessageType.error);
                return RedirectToAction(nameof(IndexMaintenance));
            }

            if (auction.idstate == 1)
            {
                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion("No permitido", "La subasta ya se encuentra publicada.", SweetAlertMessageType.warning);
                return RedirectToAction(nameof(IndexMaintenance));
            }

            await _ServiceAuctions.PublishAuction(id);
            TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion("Subasta publicada", "La subasta fue publicada y ya está activa.", SweetAlertMessageType.success);
            return RedirectToAction(nameof(IndexMaintenance));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancellAuction(int id)
        {
            var auction = await _ServiceAuctions.GetAuctionById(id);
            if (auction == null)
            {
                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion("Error", "No se encontró la subasta indicada.", SweetAlertMessageType.error);
                return RedirectToAction(nameof(IndexMaintenance));
            }

            if (auction.idstate == 4)
            {
                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion("No permitido", "La subasta ya está cancelada.", SweetAlertMessageType.warning);
                return RedirectToAction(nameof(IndexMaintenance));
            }

            if (auction.TotalBids > 0)
            {
                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion("No permitido", "No se puede cancelar una subasta que ya tiene pujas registradas.", SweetAlertMessageType.warning);
                return RedirectToAction(nameof(IndexMaintenance));
            }

            await _ServiceAuctions.CancellAuction(id);
            TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion("Subasta cancelada", "La subasta fue cancelada correctamente.", SweetAlertMessageType.success);
            return RedirectToAction(nameof(IndexMaintenance));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BanAuction(int id)
        {
            var auction = await _ServiceAuctions.GetAuctionById(id);
            if (auction == null)
            {
                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion("Error", "No se encontró la subasta indicada.", SweetAlertMessageType.error);
                return RedirectToAction(nameof(IndexMaintenance));
            }

            if (auction.idstate == 3)
            {
                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion("No permitido", "La subasta ya se encuentra baneada.", SweetAlertMessageType.warning);
                return RedirectToAction(nameof(IndexMaintenance));
            }

            await _ServiceAuctions.BanAuction(id);
            TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion("Subasta baneada", "La subasta fue baneada y removida de la plataforma.", SweetAlertMessageType.success);
            return RedirectToAction(nameof(IndexMaintenance));
        }
    }
}