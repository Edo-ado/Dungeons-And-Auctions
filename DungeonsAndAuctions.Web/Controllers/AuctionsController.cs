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
        private readonly IServiceObject _ServiceObject;
        private readonly IServiceUser _serviceUser;

        public AuctionsController(IServiceAuctions ServiceAuctions, IServiceObject ServiceObject, IServiceUser serviceUser)
        {
            _ServiceAuctions = ServiceAuctions;
            _serviceUser = serviceUser;
            _ServiceObject = ServiceObject;


        }


        public async Task<IActionResult> Index()
        {

            var collections = await _ServiceAuctions.GetSpecificViewList();


            return View(collections);
        }



        public async Task<IActionResult> Details(int id)
        {
            var auction = await _ServiceAuctions.allDetails(id);



            return View(auction);


        }
    }
}
