using D_A.Application.DTOs;
using D_A.Application.Services.Implementations;
using D_A.Application.Services.Interfaces;
using D_A.Infraestructure.Models;
using D_A.Web.Controllers;
using D_A.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;



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

            ViewBag.Objects = await _ServiceObject.ListAsync();
            ViewBag.Users = await _serviceUser.ListAsync();
            return View();

        }

        public async Task<IActionResult> Create(AuctionsDTO auction)
        {
            if (ModelState.IsValid)
            {

                //simulado
                auction.idusercreator = 2;


                await _ServiceAuctions.CreateAuction(auction);
                return RedirectToAction(nameof(Index));
            }
            return View(auction);





        }



        public async Task<IActionResult> Edit(int id)
        {

            var auction = await _ServiceAuctions.AllDetails(id);
            if (auction == null)
            {
                return NotFound();
            }
            ViewBag.Objects = await _ServiceObject.ListAsync();
            ViewBag.Users = await _serviceUser.ListAsync();
            return View(auction);

        }

        public async Task<IActionResult> Edit(AuctionsDTO auction)
        {
            if (ModelState.IsValid)
            {

                await _ServiceAuctions.EditAuction(auction);
                return RedirectToAction(nameof(Index));
            }
            return View(auction);
        }
    }
}
