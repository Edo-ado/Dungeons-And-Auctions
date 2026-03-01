using System.Diagnostics;
using D_A.Application.Services.Implementations;
using D_A.Application.Services.Interfaces;
using D_A.Web.Controllers;
using D_A.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DNDA.Web.Controllers
{
    public class ObjectController : Controller
    {
        private readonly IServiceObject _serviceObject;
        private readonly IServiceAuctions _serviceActions;




        public ObjectController(IServiceObject serviceObject, IServiceAuctions serviceAuctions)
        {
            _serviceObject = serviceObject;
            _serviceActions = serviceAuctions;



        }
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            //llama service
            var collection = await _serviceObject.ListAsync(); //recibe objecto
            return View(collection); //pasa DTOs a la vista
        }
        public async Task<IActionResult> Details(int id)
        {
            var objects = await _serviceObject.GetObjectById(id);

            var auctions = await _serviceActions.GetAuctionsByObjectID(id);

            if(auctions != null)
            {

                ViewBag.AuctionsObjects = true;
            }

            if (objects == null)

                return NotFound();

            ViewBag.AuctionsObjects = auctions;
            return View(objects);
        }

    }
}

