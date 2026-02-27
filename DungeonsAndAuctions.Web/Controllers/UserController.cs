using D_A.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DNDA.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IServiceAuctionBidHistory _serviceBid;
        private readonly IServiceUser _serviceUser;
        private readonly IServiceAuctions _serviceAuction;

        public UserController(IServiceUser serviceUser, IServiceAuctionBidHistory serviceBid, IServiceAuctions serviceAuctions)
        {
            _serviceUser = serviceUser;
            _serviceBid = serviceBid;
            _serviceAuction = serviceAuctions;

        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //llama service
            var collection = await _serviceUser.ListAsync(); //recibe usersDTO
            return View(collection); //pasa DTOs a la vista
        }
        public async Task<IActionResult> Details(int id)
        {
            var user = await _serviceUser.FindByIdAsync(id);
         
            if(user == null)
                return NotFound();

            int roleID = user.RoleId;


            if (roleID == 1)//si es comprador
            {
                int totalBids = await _serviceBid.CountBidsByBuyerAsync(id);

                ViewBag.TotalBids = totalBids;
                ViewBag.ShowPujas = true;
            }
            else if (roleID == 2)
            {
                int totalAuctions = await _serviceAuction.CountAuctionsBySellerAsync(id);
                ViewBag.TotalAuctions = totalAuctions;
                ViewBag.ShowAuction = true;

            }


            if (user == null)
                return NotFound();

            return View(user);
        }

    }
}
