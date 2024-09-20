using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shoes_EF_2024.Entidades;
using Shoes_EF_2024.Servicios.Interfaces;
using Shoes_EF_2024.Web.ViewModels.ShoeSizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoes_EF_2024.Web.Controllers
{
    public class ShoeSizesController : Controller
    {
        private readonly IServiceShoeSize? _shoeSizeService;
        private readonly IServiceSizes? _sizesService;
        private readonly IServiceShoes? _shoesService;
        private readonly IMapper? _mapper;

        public ShoeSizesController(IServiceShoeSize shoeSizeService,
                                   IServiceSizes sizesService,
                                   IServiceShoes shoesService,
                                   IMapper mapper)
        {
            _shoeSizeService = shoeSizeService ?? throw new ApplicationException("Dependencies not set");
            _sizesService = sizesService ?? throw new ApplicationException("Dependencies not set");
            _shoesService = shoesService ?? throw new ApplicationException("Dependencies not set");
            _mapper = mapper ?? throw new ApplicationException("Dependencies not set");
        }

        public IActionResult Index()
        {
            try
            {
                var shoeSizes = _shoeSizeService?.GetAll(); 
                var shoeSizesVm = _mapper?.Map<List<ShoeSizeListVm>>(shoeSizes);
                return View(shoeSizesVm);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the records.");
            }
        }

        public IActionResult UpSert(int? shoeId, int? sizeId)
        {
            ShoeSizeEditVm shoeSizeVm;
            if (shoeId == null || sizeId == null)
            {
                shoeSizeVm = new ShoeSizeEditVm
                {
                    Shoes = GetShoes(),
                    Sizes = GetSizes()
                };
            }
            else
            {
                try
                {
                    var shoeSize = _shoeSizeService?.GetAllByShoeId((int)shoeId)
                        .FirstOrDefault(ss => ss.SizeId == sizeId);
                    if (shoeSize == null)
                    {
                        return NotFound();
                    }
                    shoeSizeVm = _mapper!.Map<ShoeSizeEditVm>(shoeSize);
                    shoeSizeVm.Shoes = GetShoes();
                    shoeSizeVm.Sizes = GetSizes();
                    return View(shoeSizeVm);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the record.");
                }
            }
            return View(shoeSizeVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpSert(ShoeSizeEditVm shoeSizeVm)
        {
            if (!ModelState.IsValid)
            {
                shoeSizeVm.Shoes = GetShoes();
                shoeSizeVm.Sizes = GetSizes();
                return View(shoeSizeVm);
            }

            try
            {
                var shoeSize = _mapper!.Map<ShoeSize>(shoeSizeVm);
                if (_shoeSizeService!.Exists(shoeSize.ShoeId, shoeSize.SizeId))
                {
                    _shoeSizeService.UpdateQuantity(shoeSize.ShoeId, shoeSize.SizeId, shoeSize.QuantityInStock);
                }
                else
                {
                    _shoeSizeService.Save(shoeSize.ShoeId, shoeSize.SizeId, shoeSize.QuantityInStock);
                }
                TempData["success"] = "Record successfully added/edited.";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while editing the record.");
                shoeSizeVm.Shoes = GetShoes();
                shoeSizeVm.Sizes = GetSizes();
                return View(shoeSizeVm);
            }
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? shoeId, int? sizeId)
        {
            if (shoeId == null || sizeId == null)
            {
                return NotFound();
            }

            try
            {
                if (!_shoeSizeService.Exists((int)shoeId, (int)sizeId))
                {
                    return NotFound();
                }

                _shoeSizeService.Delete((int)shoeId, (int)sizeId);
                return Json(new { success = true, message = "Record successfully deleted." });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Couldn't delete the record!" });
            }
        }

        private List<SelectListItem> GetShoes()
        {
            return _shoesService!.GetAll(orderBy: o => o.OrderBy(s => s.Model))
                .Select(s => new SelectListItem { Text = s.Model, Value = s.ShoeId.ToString() }).ToList();
        }

        private List<SelectListItem> GetSizes()
        {
            return _sizesService!.GetAll(orderBy: o => o.OrderBy(s => s.SizeNumber))
                .Select(s => new SelectListItem { Text = s.SizeNumber.ToString(), Value = s.SizeId.ToString() }).ToList();
        }
    }
}
