using D_A.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DNDA.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IServiceUser _serviceUser;
        public UserController(IServiceUser serviceUser)
        {
            _serviceUser = serviceUser;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var collection = await _serviceUser.ListAsync();
            return View(collection);
        }
    }
}
