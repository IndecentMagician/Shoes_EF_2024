using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Shoes_EF_2024.Web.ViewModels.Brands;
using Shoes_EF_2024.Entidades;
using Shoes_EF_2024.Servicios.Interfaces;
using X.PagedList;
using Shoes_EF_2024.Web.ViewModels.Shoes;
using Microsoft.AspNetCore.Http;
using X.PagedList.Extensions;
using Shoes_EF_2024.Web.ViewModels.Colors;

namespace Shoes_EF_2024.Web.Controllers
{
    public class BrandsController : Controller
    {
        private readonly IServiceBrands _brandsService;
        private readonly IServiceShoes _shoesService;
        private readonly IMapper _mapper;

        public BrandsController(IServiceBrands brandsService, IServiceShoes shoesService, IMapper mapper)
        {
            _brandsService = brandsService ?? throw new ApplicationException("Dependencies not set");
            _shoesService = shoesService ?? throw new ApplicationException("Dependencies not set");
            _mapper = mapper ?? throw new ApplicationException("Dependencies not set");
        }

        public IActionResult Index(int? page, string? searchTerm = null, int pageSize = 10)
        {
            try
            {
                var currentPage = page ?? 1;

                var brands = _brandsService.GetAll();

                var brandListVm = brands.Select(b => new BrandListVm
                {
                    BrandId = b.BrandId,
                    BrandName = b.BrandName,
                    shoesQuantity = _shoesService.GetAll(filter: s => s.BrandId == b.BrandId).Count()
                }).ToList();

                var pagedList = brandListVm.ToPagedList(currentPage, pageSize);

                return View(pagedList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        public IActionResult UpSert(int? id)
        {
            BrandEditVm brandVm;

            if (id == null || id == 0)
            {
                brandVm = new BrandEditVm();
            }
            else
            {
                try
                {
                    var brand = _brandsService.Get(filter: c => c.BrandId == id);
                    if (brand == null)
                    {
                        return NotFound();
                    }
                    brandVm = _mapper.Map<BrandEditVm>(brand);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the record.");
                }
            }
            return View(brandVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpSert(BrandEditVm brandVm)
        {
            if (!ModelState.IsValid)
            {
                return View(brandVm);
            }

            try
            {
                var brand = _mapper.Map<Brands>(brandVm);

                if (_brandsService.Exist(brand))
                {
                    ModelState.AddModelError(string.Empty, "Record already exists");
                    return View(brandVm);
                }

                _brandsService.Save(brand);
                TempData["success"] = "Record successfully added/edited";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while editing the record.");
                return View(brandVm);
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
            var brand = _brandsService.Get(filter: c => c.BrandId == id);
            if (brand == null)
            {
                return NotFound();
            }
            try
            {
                if (_brandsService.ItsRelated(brand.BrandId))
                {
                    return Json(new { success = false, message = "Related record... Delete denied!" });
                }

                _brandsService.Delete(brand);
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

            var brand = _brandsService.Get(filter: c => c.BrandId == id);
            if (brand == null)
            {
                return NotFound();
            }

            var currentPage = page ?? 1;
            int pageSize = 10;
            var brandVm = _mapper.Map<BrandDetailsVm>(brand);
            brandVm.ShoesQuantity = _shoesService.GetAll(filter: s => s.BrandId == brandVm.BrandId).Count();

            var shoes = _shoesService.GetAll(
                orderBy: o => o.OrderBy(p => p.Model),
                filter: p => p.BrandId == brandVm.BrandId,
                propertiesNames: "Brands");

            brandVm.Shoes = _mapper.Map<List<ShoeListVm>>(shoes).ToPagedList(currentPage, pageSize);

            return View(brandVm);
        }
    }
}

