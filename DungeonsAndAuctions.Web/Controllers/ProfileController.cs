using D_A.Application.Services.Interfaces;
using D_A.Infraestructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DNDA.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IServiceUser _serviceUser;

        public ProfileController(IServiceUser serviceUser)
        {
            _serviceUser = serviceUser;
        }

        private int GetUserId() =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();

            var profile = await _serviceUser.GetProfileAsync(userId);
            if (profile == null) return NotFound();

            var bids = await _serviceUser.GetBidHistoryAsync(userId);
            var auctions = await _serviceUser.GetUserAuctionsAsync(userId);
            var payments = await _serviceUser.GetUserPaymentsAsync(userId);

            ViewBag.Bids = bids;
            ViewBag.Auctions = auctions;
            ViewBag.Payments = payments;

            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(
            string firstName, string lastName,
            string? phoneNumber, string? aboutMe)
        {
            var userId = GetUserId();

            var success = await _serviceUser.UpdateProfileAsync(
                userId, firstName, lastName, phoneNumber, aboutMe);

            TempData[success ? "Success" : "Error"] = success
                ? "Perfil actualizado correctamente."
                : "Error al actualizar el perfil.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(
            string currentPassword, string newPassword, string confirmNewPassword)
        {
            var userId = GetUserId();

            if (newPassword != confirmNewPassword)
            {
                TempData["Error"] = "Las contraseñas nuevas no coinciden.";
                return RedirectToAction("Index");
            }

            var success = await _serviceUser.ChangePasswordAsync(
                userId, currentPassword, newPassword);

            TempData[success ? "Success" : "Error"] = success
                ? "Contraseña actualizada correctamente."
                : "La contraseña actual es incorrecta.";

            return RedirectToAction("Index");
        }
    }
}