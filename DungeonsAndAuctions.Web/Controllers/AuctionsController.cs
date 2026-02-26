using D_A.Application.Services.Implementations;
using D_A.Application.Services.Interfaces;
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

        public AuctionsController(IServiceAuctions ServiceAuctions)
        {
            _ServiceAuctions = ServiceAuctions;


        }



        public async Task<IActionResult> Index()
        {

            var collections = await _ServiceAuctions.GetAllAuctions();


            return View(collections);
        }


        


    }
}
