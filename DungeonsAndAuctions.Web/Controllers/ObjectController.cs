using System.Diagnostics;
using D_A.Application.DTOs;
using D_A.Application.Services.Implementations;
using D_A.Application.Services.Interfaces;
using D_A.Infraestructure.Models;
using D_A.Web.Controllers;
using D_A.Web.Models;
using DNDA.Web.Extensions; // <-- añadido
using DNDA.Web.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DNDA.Web.Controllers
{

    [Authorize]
    public class ObjectController : Controller
    {
        private readonly IServiceObject _serviceObject;
        private readonly IServiceAuctions _serviceActions;
        private readonly IServiceCategories _serviceCategorias;
        private readonly IServiceQuality _serviceQuality;
        private readonly IServiceUser _ServiceUser;

        public ObjectController(IServiceObject serviceObject, IServiceAuctions serviceAuctions, IServiceCategories serviceCategories, IServiceQuality serviceQuality, IServiceUser serviceUser)
        {
            _serviceObject = serviceObject;
            _serviceActions = serviceAuctions;
            _serviceCategorias = serviceCategories;
            _serviceQuality = serviceQuality;
            _ServiceUser = serviceUser;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Vendedor,Admin")]
        public async Task<ActionResult> ToggleActive(int id)
        {


            //busco si tiene subastas activas
            var hasActive = await _serviceObject.HasActiveAuctionAsync(id);
            if (hasActive)
            {
                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                    "No permitido",
                    "Este objeto pertenece a una subasta activa y no puede ser desactivado.",
                    SweetAlertMessageType.warning
                );
                return RedirectToAction(nameof(Index));
            }

            //busco si ha participado en alguna subasta anteriormente, sin importan si está activa o no
            var hasBeenAuctioned = await _serviceObject.HasBeenAuctionedAsync(id);
            if (hasBeenAuctioned)
            {
                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                    "No permitido",
                    "Este objeto ha sido subastado y no puede ser desactivado.",
                    SweetAlertMessageType.warning
                );
                return RedirectToAction(nameof(Index));
            }



            await _serviceObject.ToggleActiveAsync(id);
            TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                "Estado actualizado",
                "El estado del objeto fue modificado correctamente.",
                SweetAlertMessageType.success
            );
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        [Authorize(Roles = "Vendedor,Admin")]
        public async Task<IActionResult> Index()
        {
            var collection = await _serviceObject.ListAsync();

            // Vendedor solo ve sus propios objetos; Admin ve todos
            if (!User.IsInRole("Admin"))
            {
                var currentUser = HttpContext.Session.GetObject<UsersDTO>(AccountController.SessionKeyUser);
                if (currentUser != null)
                    collection = collection.Where(o => o.UserId == currentUser.Id).ToList();
            }

            return View(collection);
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var objects = await _serviceObject.GetObjectById(id);

            var auctions = await _serviceActions.GetAuctionsByObjectID(id);

            if (auctions != null)
            {

                ViewBag.AuctionsObjects = true;
            }

            if (objects == null)

                return NotFound();

            ViewBag.AuctionsObjects = auctions;
            return View(objects);
        }


        // -------------------------
        // Helpers para combos
        // -------------------------
        private async Task LoadCombosAsync(IEnumerable<string>? selectedCategoriaIds = null)
        {
            var qualities = await _serviceQuality.ListAsync();
            ViewBag.ListQuality = new SelectList(
                items: qualities,
                dataValueField: nameof(QualitiesDTO.Id),
                dataTextField: nameof(QualitiesDTO.Quality)
            );

            var categorias = await _serviceCategorias.ListAsync();
            ViewBag.ListCategorias = new MultiSelectList(
                items: categorias,
                dataValueField: nameof(CategoriesDTO.Id),
                dataTextField: nameof(CategoriesDTO.Name),
                selectedValues: selectedCategoriaIds
            );
        }

        // GET: ObjectController/Create
        [Authorize(Roles = "Vendedor,Admin")]
        public async Task<ActionResult> Create()
        {
            await LoadCombosAsync();

            // Obtener usuario real desde sesión
            var currentUser = HttpContext.Session.GetObject<UsersDTO>(AccountController.SessionKeyUser);
            if (currentUser == null || User?.Identity?.IsAuthenticated != true)
            {
                // No hay usuario en sesión: redirigir para iniciar sesión
                return RedirectToAction("Login", "Account");
            }

            ViewBag.UserName = currentUser.UserName;
            ViewBag.UserId = currentUser.Id;
            return View(new ObjectsDTO());
        }


        // POST: LibroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Vendedor,Admin")]
        public async Task<ActionResult> Create(ObjectsDTO dto, List<IFormFile> imagenes, string[] selectedCategorias)
        {
            selectedCategorias ??= Array.Empty<string>();
            //Validación de categorías 
            if (selectedCategorias.Length == 0)
            {
                ModelState.AddModelError("selectedCategorias", "Debe seleccionar al menos una categoría.");
            }

            //Imagen requerida en Create
            if (imagenes == null || !imagenes.Any())
            {
                ModelState.AddModelError("imagenes", "Debe seleccionar al menos una imagen.");
            }

            //Si se envia imagen, convertirla a byte[]
            if (imagenes != null && imagenes.Any())
            {
                dto.Imagenes = new List<byte[]>();
                foreach (var img in imagenes)
                {
                    using var ms = new MemoryStream();
                    await img.CopyToAsync(ms);
                    dto.Imagenes.Add(ms.ToArray());
                }
                ModelState.Remove("Imagenes");
            }

            ModelState.Remove("imageFile");

            if (!ModelState.IsValid)
            {
                var errores = string.Join("<br>",
                    ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                );

                ViewBag.Notificacion = SweetAlertHelper.CrearNotificacion(
                    "Errores de validación",
                    $"El formulario contiene errores:<br>{errores}",
                    SweetAlertMessageType.warning
                );
                await LoadCombosAsync(selectedCategorias);
                return View(dto);
            }

            var categoryIds = selectedCategorias
                .Select(x => int.Parse(x))
                .ToList();

            // Obtener usuario real desde sesión (en vez de hardcodear)
            var currentUser = HttpContext.Session.GetObject<UsersDTO>(AccountController.SessionKeyUser);
            if (currentUser == null || User?.Identity?.IsAuthenticated != true)
            {
                // Si no hay usuario, redirigir a login (o manejar según flujo)
                return RedirectToAction("Login", "Account");
            }

            dto.UserId = currentUser.Id;
            dto.IsActive = true;
            dto.RegistrationDate = DateOnly.FromDateTime(DateTime.Now);

            await _serviceObject.AddAsync(dto, categoryIds, dto.Imagenes);

            TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                "Objeto creado correctamente",
                $"El objeto {dto.Name} fue registrado exitosamente.",
                SweetAlertMessageType.success
            );

            return RedirectToAction(nameof(Index));
        }


        // GET: ObjectController/Edit/5
        [Authorize(Roles = "Vendedor,Admin")]
        public async Task<ActionResult> Edit(int id)
        {

            var hasActive = await _serviceObject.HasActiveAuctionAsync(id);
            if (hasActive)
            {
                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                    "No permitido",
                    "Este objeto está asociado a una subasta y no puede ser editado.",
                    SweetAlertMessageType.warning
                );
                return RedirectToAction(nameof(Index));
            }

            var dto = await _serviceObject.GetObjectById(id);

            var selected = dto.Categories.Select(c => c.Id.ToString()).ToList();
            dto.IsActive = dto.IsActive;

            //obtener info del usuario asignado al objeto
            var UserAsigned = await _ServiceUser.FindByIdAsync(dto.UserId);
            ViewBag.UserName = UserAsigned?.UserName;
            ViewBag.UserID = UserAsigned?.Id;



            await LoadCombosAsync(selected);
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Vendedor,Admin")]
        public async Task<ActionResult> Edit(int id, ObjectsDTO dto, List<IFormFile>? imagenes, string[] selectedCategorias)
        {

            var hasActive = await _serviceObject.HasActiveAuctionAsync(id);
            if (hasActive)
            {
                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                    "No permitido",
                    "Este objeto está asociado a una subasta y no puede ser editado.",
                    SweetAlertMessageType.warning
                );
                return RedirectToAction(nameof(Index));
            }


            selectedCategorias ??= Array.Empty<string>();

            if (selectedCategorias.Length == 0)
                ModelState.AddModelError("selectedCategorias", "Debe seleccionar al menos una categoría.");

            //Convertir imágenes nuevas si se suben
            var imagenesBytes = new List<byte[]>();


            if (imagenes != null && imagenes.Count > 0)
            {
                foreach (var file in imagenes)
                {
                    using var ms = new MemoryStream();
                    await file.CopyToAsync(ms);
                    imagenesBytes.Add(ms.ToArray());
                }
                ModelState.Remove("imagenes");
            }



            var categoryIds = selectedCategorias.Select(x => int.Parse(x)).ToList();
            dto.IsActive = dto.IsActive;
            await _serviceObject.UpdateAsync(id, dto, categoryIds, imagenesBytes);

            TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                "Objeto actualizado",
                $"El objeto {dto.Name} fue modificado exitosamente.",
                SweetAlertMessageType.success
            );

            return RedirectToAction(nameof(Index));
        }




    }




}