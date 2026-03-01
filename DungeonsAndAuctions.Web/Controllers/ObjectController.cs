using System.Diagnostics;
using D_A.Application.Services.Interfaces;
using D_A.Web.Controllers;
using D_A.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DNDA.Web.Controllers
{
    public class ObjectController : Controller
    {
        private readonly IServiceObject _serviceObject;
    


        public ObjectController(IServiceObject serviceObject)
        {
            _serviceObject = serviceObject;
         

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
            if (objects == null)
                return NotFound();

            return View(objects);
        }

    }
}

