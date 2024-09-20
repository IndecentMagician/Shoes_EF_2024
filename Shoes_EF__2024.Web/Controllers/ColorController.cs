using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Shoes_EF_2024.Entidades;
using Shoes_EF_2024.Servicios.Interfaces;
using X.PagedList;
using Shoes_EF_2024.Web.ViewModels.Shoes;
using Microsoft.AspNetCore.Http;
using X.PagedList.Extensions;
using Shoes_EF_2024.Web.ViewModels.Colors;

namespace Shoes_EF_2024.Web.Controllers
{
    public class ColorsController : Controller
    {
        private readonly IServiceColors _colorService;
        private readonly IServiceShoes _shoesService;
        private readonly IMapper _mapper;

        public ColorsController(IServiceColors colorService, IServiceShoes shoesService, IMapper mapper)
        {
            _colorService = colorService ?? throw new ApplicationException("Dependencies not set");
            _shoesService = shoesService ?? throw new ApplicationException("Dependencies not set");
            _mapper = mapper ?? throw new ApplicationException("Dependencies not set");
        }

        public IActionResult Index(int? page, string? searchTerm = null, int pageSize = 10)
        {
            try
            {
                var currentPage = page ?? 1;

                var colorList = _colorService.GetAll();

                var colorsListVm = colorList.Select(b => new ColorlistVm
                {
                    ColorId = b.ColorId,
                    ColorName = b.ColorName,
                    shoesQuantity = _shoesService.GetAll(filter: s => s.ColorID == b.ColorId).Count()
                }).ToList();

                var pagedList = colorsListVm.ToPagedList(currentPage, pageSize);

                return View(pagedList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        public IActionResult UpSert(int? id)
        {
            ColorEditVm colorEdVm;

            if (id == null || id == 0)
            {
                colorEdVm = new ColorEditVm();
            }
            else
            {
                try
                {
                    var color = _colorService.Get(filter: c => c.ColorId == id);
                    if (color == null)
                    {
                        return NotFound();
                    }
                    colorEdVm = _mapper.Map<ColorEditVm>(color);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the record.");
                }
            }
            return View(colorEdVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpSert(ColorEditVm colorVm)
        {
            if (!ModelState.IsValid)
            {
                return View(colorVm);
            }

            try
            {
                var color = _mapper.Map<Colors>(colorVm);

                if (_colorService.Exist(color))
                {
                    ModelState.AddModelError(string.Empty, "Record already exists");
                    return View(colorVm);
                }

                _colorService.Save(color);
                TempData["success"] = "Record successfully added/edited";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while editing the record.");
                return View(colorVm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var color = _colorService.Get(filter: c => c.ColorId == id);
            if (color == null)
            {
                return NotFound();
            }

            try
            {
                if (_colorService.ItsRelated(color.ColorId))
                {
                    return Json(new { success = false, message = "Related record... Delete denied!" });
                }

                _colorService.Delete(color);
                return Json(new { success = true, message = "Record successfully deleted" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Couldn't delete the record!" });
            }
        }

        public IActionResult Details(int? id, int? page)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var color = _colorService.Get(filter: c => c.ColorId == id);
            if (color == null)
            {
                return NotFound();
            }

            var currentPage = page ?? 1;
            int pageSize = 10;
            var colorVm = _mapper.Map<ColorDetailsVm>(color);
            colorVm.ShoesQuantity = _shoesService.GetAll(filter: s => s.ColorID == colorVm.ColorId).Count();

            var shoes = _shoesService.GetAll(
                orderBy: o => o.OrderBy(p => p.Model),
                filter: p => p.ColorID == colorVm.ColorId,
                propertiesNames: "Colors");

            colorVm.Shoes = _mapper.Map<List<ShoeListVm>>(shoes).ToPagedList(currentPage, pageSize);

            return View(colorVm);
        }
    }
}

