using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shoes_EF_2024.Entidades;
using Shoes_EF_2024.Servicios.Interfaces;
using Shoes_EF_2024.Web.ViewModels.Shoes;
using Shoes_EF_2024.Web.ViewModels.ShoeSizes;
using X.PagedList.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shoes_EF_2024.Web.ViewModels;

namespace Shoes_EF_2024.Web.Controllers
{
    public class ShoesController : Controller
    {
        private readonly IServiceShoes? _shoesService;
        private readonly IServiceBrands? _brandsService;
        private readonly IServiceGenres? _genresService;
        private readonly IServiceSports? _sportsService;
        private readonly IServiceColors? _colorsService;
        private readonly IServiceSizes? _sizesService;
        private readonly IServiceShoeSize? _shoeSizeService;
        private readonly IMapper? _mapper;

        private int pageSize = 10;

        public ShoesController(IServiceShoes shoesService,
                               IServiceBrands brandsService,
                               IServiceGenres genresService,
                               IServiceSports sportsService,
                               IServiceColors colorsService,
                               IServiceSizes sizesService,
                               IServiceShoeSize shoeSizeService,
                               IMapper mapper)
        {
            _shoesService = shoesService ?? throw new ArgumentNullException(nameof(shoesService));
            _brandsService = brandsService ?? throw new ArgumentNullException(nameof(brandsService));
            _genresService = genresService ?? throw new ArgumentNullException(nameof(genresService));
            _sportsService = sportsService ?? throw new ArgumentNullException(nameof(sportsService));
            _colorsService = colorsService ?? throw new ArgumentNullException(nameof(colorsService));
            _sizesService = sizesService ?? throw new ArgumentNullException(nameof(sizesService));
            _shoeSizeService = shoeSizeService ?? throw new ArgumentNullException(nameof(shoeSizeService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public IActionResult Index(int? page, int? filterBrandId, int? filterSportId, int? filterGenreId, int? filterColorId, int pageSize = 10, bool viewAll = false)
        {
            try
            {
                var currentPage = page ?? 1;
                ViewBag.currentPageSize = pageSize;
                ViewBag.currentFilterBrandId = filterBrandId;
                ViewBag.currentFilterSportId = filterSportId;
                ViewBag.currentFilterGenreId = filterGenreId;
                ViewBag.currentFilterColorId = filterColorId;

                IEnumerable<Shoes>? shoes;
                if (viewAll)
                {
                    shoes = _shoesService?.GetAll(
                        orderBy: o => o.OrderBy(s => s.Model),
                        propertiesNames: "Brand,Sport,Genre,Color");
                }
                else
                {
                    shoes = _shoesService?.GetAll(
                        orderBy: o => o.OrderBy(s => s.Model),
                        filter: s =>
                            (filterBrandId == null || s.BrandId == filterBrandId) &&
                            (filterSportId == null || s.SportId == filterSportId) &&
                            (filterGenreId == null || s.GenreId == filterGenreId) &&
                            (filterColorId == null || s.ColorID == filterColorId),
                        propertiesNames: "Brand,Sport,Genre,Color");
                }

                var shoeListVm = _mapper?.Map<List<ShoeListVm>>(shoes);
                var shoeFilterVm = new ShoeFilterVm()
                {
                    Shoes = shoeListVm?.ToPagedList(currentPage, pageSize),
                    Brands = GetBrands(),
                    Sports = GetSports(),
                    Genres = GetGenres(),
                    Colors = GetColors()
                };

                return View(shoeFilterVm);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }




        public IActionResult UpSert(int? id)
        {
            ShoeEditVm shoeVm;

            if (id == null || id == 0)
            {
                shoeVm = new ShoeEditVm
                {
                    Brands = GetBrands(),
                    Genres = GetGenres(),
                    Sports = GetSports(),
                    Colors = GetColors(),
                    Sizes = GetSizes()
                };
            }
            else
            {
                try
                {
                    var shoe = _shoesService!.Get(s => s.ShoeId == id, propertiesNames: "Brand,Genre,Sport,Color,Sizes");
                    if (shoe == null)
                    {
                        return NotFound("Shoe does not exist.");
                    }
                    shoeVm = _mapper!.Map<ShoeEditVm>(shoe);
                    shoeVm.Brands = GetBrands();
                    shoeVm.Genres = GetGenres();
                    shoeVm.Sports = GetSports();
                    shoeVm.Colors = GetColors();
                    shoeVm.Sizes = GetSizes();
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error obtaining Shoe.");
                }
            }

            return View(shoeVm);
        }


        public IActionResult Details(int id)
        {
            try
            {
                var shoe = _shoesService?.Get(s => s.ShoeId == id, propertiesNames: "Brand,Sport,Genre,Color,Size");
                if (shoe == null)
                {
                    return NotFound();
                }

                var shoeDetailsVm = _mapper!.Map<ShoeDetailsVm>(shoe);

                var shoeSizes = _shoeSizeService?.GetAllByShoeId(id);
                shoeDetailsVm.ShoeSizes = shoeSizes?.Select(ss => new ShoeSizeListVm
                {
                    SizeId = ss.SizeId,
                    ShoeId = ss.ShoeId,
                    QuantityInStock = ss.QuantityInStock
                }).ToList();

                shoeDetailsVm.NumberOfSizes = shoeSizes?.Count ?? 0;

                return View(shoeDetailsVm);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the record.");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Sell(int shoeId, int sizeId, int quantity)
        {
            try
            {
                var shoeSize = _shoeSizeService?.GetAllByShoeId(shoeId)
                    .FirstOrDefault(ss => ss.SizeId == sizeId);

                if (shoeSize != null && shoeSize.QuantityInStock >= quantity)
                {
                    _shoeSizeService.UpdateQuantity(shoeId, sizeId, shoeSize.QuantityInStock - quantity);
                    TempData["success"] = "Stock successfully updated.";
                }
                else
                {
                    TempData["error"] = "Insufficient stock or item not found.";
                }

                return RedirectToAction("Details", new { id = shoeId });
            }
            catch (Exception)
            {
                TempData["error"] = "An error occurred while updating the stock.";
                return RedirectToAction("Details", new { id = shoeId });
            }
        }

        private List<SelectListItem> GetBrands()
        {
            return _brandsService!.GetAll(orderBy: o => o.OrderBy(b => b.BrandName))
                .Select(b => new SelectListItem { Text = b.BrandName, Value = b.BrandId.ToString() }).ToList();
        }

        private List<SelectListItem> GetGenres()
        {
            return _genresService!.GetAll(orderBy: o => o.OrderBy(g => g.GenreName))
                .Select(g => new SelectListItem { Text = g.GenreName, Value = g.GenreId.ToString() }).ToList();
        }

        private List<SelectListItem> GetSports()
        {
            return _sportsService!.GetAll(orderBy: o => o.OrderBy(s => s.SportName))
                .Select(s => new SelectListItem { Text = s.SportName, Value = s.SportId.ToString() }).ToList();
        }

        private List<SelectListItem> GetColors()
        {
            return _colorsService!.GetAll(orderBy: o => o.OrderBy(c => c.ColorName))
                .Select(c => new SelectListItem { Text = c.ColorName, Value = c.ColorId.ToString() }).ToList();
        }

        private List<SelectListItem> GetSizes()
        {
            return _sizesService!.GetAll(orderBy: o => o.OrderBy(s => s.SizeNumber))
                .Select(s => new SelectListItem { Text = s.SizeNumber.ToString(), Value = s.SizeId.ToString() }).ToList();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpSert(ShoeEditVm shoeVm)
        {
            if (!ModelState.IsValid)
            {
                shoeVm.Brands = GetBrands();
                shoeVm.Genres = GetGenres();
                shoeVm.Sports = GetSports();
                shoeVm.Colors = GetColors();
                shoeVm.Sizes = GetSizes();

                return View(shoeVm);
            }

            try
            {
                var shoe = _mapper!.Map<Shoes>(shoeVm);

                if (_shoesService!.Exist(shoe))
                {
                    ModelState.AddModelError(string.Empty, "The shoe already exists.");
                    shoeVm.Brands = GetBrands();
                    shoeVm.Genres = GetGenres();
                    shoeVm.Sports = GetSports();
                    shoeVm.Colors = GetColors();
                    shoeVm.Sizes = GetSizes();
                    return View(shoeVm);
                }

                _shoesService.Save(shoe);
                TempData["success"] = "Record successfully added/edited.";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while saving the record.");
                shoeVm.Brands = GetBrands();
                shoeVm.Genres = GetGenres();
                shoeVm.Sports = GetSports();
                shoeVm.Colors = GetColors();
                shoeVm.Sizes = GetSizes();
                return View(shoeVm);
            }
        }


        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var shoe = _shoesService?.Get(s => s.ShoeId == id);
            if (shoe == null)
            {
                return NotFound();
            }
            try
            {
                if (_shoesService!.ItsRelated(shoe.ShoeId))
                {
                    return Json(new { success = false, message = "Related record... Delete denied!" });
                }

                _shoesService.Delete(shoe);
                return Json(new { success = true, message = "Record successfully deleted." });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Couldn't delete the record!" });
            }
        }
    }
}
