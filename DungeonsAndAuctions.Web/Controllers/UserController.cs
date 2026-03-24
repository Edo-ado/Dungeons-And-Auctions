using D_A.Application.DTOs;
using D_A.Application.Services.Interfaces;
using D_A.Infraestructure.Models;
using DNDA.Web.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace DNDA.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IServiceAuctionBidHistory _serviceBid;
        private readonly IServiceUser _serviceUser;
        private readonly IServiceAuctions _serviceAuction;
        private readonly IServiceCountry _serviceCountry;
        private readonly IServiceGender _serviceGenero;


        public UserController(IServiceUser serviceUser, IServiceAuctionBidHistory serviceBid, IServiceAuctions serviceAuctions, IServiceCountry serviceCountry, IServiceGender serviceGender)
        {
            _serviceUser = serviceUser;
            _serviceBid = serviceBid;
            _serviceAuction = serviceAuctions;
            _serviceCountry = serviceCountry;
            _serviceGenero = serviceGender;


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleBlock(int id)
        {
            var user = await _serviceUser.FindByIdAsync(id);
            if (user == null) return NotFound();

            await _serviceUser.ToggleBlockAsync(id);

            var accion = user.IsBlocked ? "desbloqueado" : "bloqueado";
            TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                "Estado actualizado",
                $"El usuario {user.UserName} fue {accion} correctamente.",
                SweetAlertMessageType.success
            );
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var user = await _serviceUser.FindByIdAsync(id);
            if (user == null) return NotFound();

            await _serviceUser.ToggleActiveAsync(id);

            var accion = user.Active ? "desactivado" : "activado";
            TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                "Estado actualizado",
                $"El usuario {user.UserName} fue {accion} correctamente.",
                SweetAlertMessageType.success
            );
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var collection = await _serviceUser.ListAsync();
            return View(collection);
        }
        public async Task<IActionResult> Details(int id)
        {
            var user = await _serviceUser.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            int roleID = user.RoleId;


            if (roleID == 1) //si es comprador
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
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _serviceUser.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            int roleID = user.RoleId;

            if (user == null)
                return NotFound();


            return View(user);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _serviceUser.FindByIdAsync(id);
            if (user == null)

                return NotFound();

            await LoadCombosAsync(
                SelectedGenre: new[] { user.GenderId.ToString() },
                SelectedCountry: new[] { user.CountryId.ToString() }
            );

            int roleID = user.RoleId;


            if (roleID == 1) //si es comprador
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UsersDTO dto)
        {

     

            if (!ModelState.IsValid)
            {
                var errores = string.Join("<br>",
                    ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                );

                await LoadCombosAsync(
                    SelectedGenre: new[] { dto.GenderId.ToString() },
                    SelectedCountry: new[] { dto.CountryId.ToString() }
                );

               
               
           

                return View(dto);
            }

            TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                   "Estado actualizado",
                   $"El usuario se actualizo correctamente",
                   SweetAlertMessageType.success
               );

            await _serviceUser.UpdateAsync(id, dto);

            return RedirectToAction(nameof(Index));
        }
        private async Task LoadCombosAsync(IEnumerable<string>? SelectedGenre = null, IEnumerable<string>? SelectedCountry = null)
        {
            // Genero (one-to-many)
            var ListGenero = await _serviceGenero.ListAsync();

            // Pais (one-to-many)
            var Pais = await _serviceCountry.ListAsync();

            ViewBag.ListGenero = new SelectList(
                items: ListGenero,
                dataValueField: nameof(GendersDTO.Id),
                dataTextField: nameof(GendersDTO.Name),
                selectedValue: SelectedGenre

                );

            ViewBag.ListPais = new SelectList(
                items: Pais,
                dataValueField: nameof(CountriesDTO.Id),
                dataTextField: nameof(CountriesDTO.Name),
                selectedValue: SelectedCountry

            );
        }


    }
}