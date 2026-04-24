

using D_A.Application.DTOs;
using D_A.Application.Services.Interfaces;
using DNDA.Web.Extensions;
using DNDA.Web.Models;
using DNDA.Web.Util;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace DNDA.Web.Controllers
{


    public class AccountController : Controller
    {
        private readonly IServiceUser _serviceUser;
        private readonly IServiceCountry _serviceCountry;
        private readonly IServiceGender _serviceGender;
        private readonly ILogger<AccountController> _logger;

     
        public const string SessionKeyUser = "CurrentUser";

        public AccountController(
            IServiceUser serviceUser,
            IServiceCountry serviceCountry,
            IServiceGender serviceGender,
            ILogger<AccountController> logger)
        {
            _serviceUser = serviceUser;
            _serviceCountry = serviceCountry;
            _serviceGender = serviceGender;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
      
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                string errores = string.Join("<br>", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                ViewBag.Notificacion = SweetAlertHelper.CrearNotificacion(
                    "Errores de validación",
                    $"Por favor corrige:<br>{errores}",
                    SweetAlertMessageType.warning);

                _logger.LogWarning("Login: errores de validación para {Email}", model.Email);
                return View(model);
            }

 
            var userDto = await _serviceUser.LoginAsync(model.Email, model.Password);

            if (userDto == null)
            {
                ViewBag.Notificacion = SweetAlertHelper.CrearNotificacion(
                    "Acceso denegado",
                    "El correo o la contraseña son incorrectos, o tu cuenta está inactiva.",
                    SweetAlertMessageType.warning);

                _logger.LogWarning("Login fallido para email: {Email}", model.Email);
                return View(model);
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
                new(ClaimTypes.Name,           $"{userDto.FirstName} {userDto.LastName}"),
                new(ClaimTypes.Email,          userDto.Email),
                new(ClaimTypes.Role,           userDto.RoleName ?? userDto.RoleId.ToString()),
                new("UserName",                userDto.UserName),
                new("UserId",                  userDto.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = false  
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

           
            HttpContext.Session.SetObject(SessionKeyUser, userDto);

            _logger.LogInformation("Login exitoso para {Email} (Id={Id})", userDto.Email, userDto.Id);

            TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                "¡Bienvenido al Reino!",
                $"Has iniciado sesión como <strong>{userDto.FirstName}</strong>.",
                SweetAlertMessageType.success);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");

            await LoadRegisterCombosAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                string errores = string.Join("<br>", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                ViewBag.Notificacion = SweetAlertHelper.CrearNotificacion(
                    "Errores de validación",
                    $"Por favor corrige:<br>{errores}",
                    SweetAlertMessageType.warning);

                await LoadRegisterCombosAsync(model.CountryId, model.GenderId);
                return View(model);
            }

      
            var registerDto = new RegisterUserDTO
            {
                FirstName = model.FirstName.Trim(),
                LastName = model.LastName.Trim(),
                UserName = model.UserName.Trim(),
                Email = model.Email.Trim().ToLower(),
                Password = model.Password,
                BirthDate = model.BirthDate,
                CountryId = model.CountryId,
                GenderId = model.GenderId,
                RoleId = model.RoleId,
                PhoneNumber = model.PhoneNumber?.Trim(),
                AboutMe = model.AboutMe?.Trim()
            };

            var newUser = await _serviceUser.RegisterAsync(registerDto);

            if (newUser == null)
            {
                
                ModelState.AddModelError("Email", "Este correo ya está registrado en el reino.");
                ViewBag.Notificacion = SweetAlertHelper.CrearNotificacion(
                    "Correo duplicado",
                    "Ese correo ya pertenece a otro aventurero del reino.",
                    SweetAlertMessageType.warning);

                await LoadRegisterCombosAsync(model.CountryId, model.GenderId);
                _logger.LogWarning("Registro fallido: email duplicado {Email}", model.Email);
                return View(model);
            }

            _logger.LogInformation("Nuevo usuario registrado: {Email} (Id={Id})", newUser.Email, newUser.Id);

            TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                "¡Registro exitoso!",
                $"Bienvenido, <strong>{newUser.FirstName}</strong>. Ya puedes iniciar sesión.",
                SweetAlertMessageType.success);

            return RedirectToAction(nameof(Login));
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            string userName = User.Identity?.Name ?? "desconocido";

        
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Clear();

            _logger.LogInformation("Logout de {User}", userName);

            TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                "Sesión finalizada",
                "Has abandonado el reino. Hasta la próxima batalla.",
                SweetAlertMessageType.success);

            return RedirectToAction(nameof(Login));
        }

     
        [HttpGet]
        public IActionResult Forbidden()
        {
            return View();
        }

   
        private async Task LoadRegisterCombosAsync(int selectedCountry = 0, int selectedGender = 0)
        {
            var countries = await _serviceCountry.ListAsync();
            var genders = await _serviceGender.ListAsync();

            ViewBag.ListPais = new SelectList(
                items: countries,
                dataValueField: nameof(CountriesDTO.Id),
                dataTextField: nameof(CountriesDTO.Name),
                selectedValue: selectedCountry == 0 ? null : (object)selectedCountry);

            ViewBag.ListGenero = new SelectList(
                items: genders,
                dataValueField: nameof(GendersDTO.Id),
                dataTextField: nameof(GendersDTO.Name),
                selectedValue: selectedGender == 0 ? null : (object)selectedGender);
        }





    }
}