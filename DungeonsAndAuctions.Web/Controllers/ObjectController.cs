using System.Diagnostics;
using D_A.Application.DTOs;
using D_A.Application.Services.Implementations;
using D_A.Application.Services.Interfaces;
using D_A.Infraestructure.Models;
using D_A.Web.Controllers;
using D_A.Web.Models;
using DNDA.Web.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DNDA.Web.Controllers
{
    public class ObjectController : Controller
    {
        private readonly IServiceObject _serviceObject;
        private readonly IServiceAuctions _serviceActions;
        private readonly IServiceCategories _serviceCategorias;
        private readonly IServiceQuality _serviceQuality;

  

        public ObjectController(IServiceObject serviceObject, IServiceAuctions serviceAuctions, IServiceCategories serviceCategories, IServiceQuality serviceQuality)
        {
            _serviceObject = serviceObject;
            _serviceActions = serviceAuctions;
            _serviceCategorias = serviceCategories;
            _serviceQuality = serviceQuality;
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
        public async Task<ActionResult> Create()
        {
            await LoadCombosAsync();
            return View(new ObjectsDTO());
        }


        // POST: LibroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                //Recopilar todos los errores del ModelState
                var errores = string.Join("<br>",
                    ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                );

                // Notificación SweetAlert con el detalle de errores
                ViewBag.Notificacion = SweetAlertHelper.CrearNotificacion(
                    "Errores de validación",
                    $"El formulario contiene errores:<br>{errores}",
                    SweetAlertMessageType.warning
                );
                // Importante: Recargar combos antes de retornar vista
                await LoadCombosAsync(selectedCategorias);
                return View(dto);
            }

            //convierto categorias a list int
            var categoryIds = selectedCategorias
                .Select(x => int.Parse(x))
                .ToList();

            //usuario falso
            dto.UserId = 1;
            dto.IsActive = true;
            dto.RegistrationDate = DateOnly.FromDateTime(DateTime.Now);


            await _serviceObject.AddAsync(dto, categoryIds, dto.Imagenes);



            //Notificar creación
            TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                "Objeto creado correctamente",
                $"El objeto {dto.Name} fue registrado exitosamente.",
                SweetAlertMessageType.success
            );

            return RedirectToAction(nameof(Index));
        }


        // GET: ObjectController/Edit/5
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

            var selected = dto.Categories
                .Select(c => c.Id.ToString())
                .ToList();

            await LoadCombosAsync(selected);
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ObjectsDTO dto, List<IFormFile>? imagenes, string[] selectedCategorias)
        {
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

            var categoryIds = selectedCategorias.Select(x => int.Parse(x)).ToList();
            dto.IsActive = true;
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

