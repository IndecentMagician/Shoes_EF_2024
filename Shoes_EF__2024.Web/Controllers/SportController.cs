using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Shoes_EF_2024.Entidades;
using Shoes_EF_2024.Servicios.Interfaces;
using X.PagedList;
using Shoes_EF_2024.Web.ViewModels.Shoes;
using Microsoft.AspNetCore.Http;
using X.PagedList.Extensions;
using Shoes_EF_2024.Web.ViewModels.Sports;

namespace Shoes_EF_2024.Web.Controllers
{
    public class SportsController : Controller
    {
        private readonly IServiceSports _sportService;
        private readonly IServiceShoes _shoesService;
        private readonly IMapper _mapper;

        public SportsController(IServiceSports sportsService, IServiceShoes shoesService, IMapper mapper)
        {
            _sportService = sportsService ?? throw new ApplicationException("Dependencies not set");
            _shoesService = shoesService ?? throw new ApplicationException("Dependencies not set");
            _mapper = mapper ?? throw new ApplicationException("Dependencies not set");
        }

        public IActionResult Index(int? page, string? searchTerm = null, int pageSize = 10)
        {
            try
            {
                var currentPage = page ?? 1;

                var sports = _sportService.GetAll();

                var sportsListVm = sports.Select(b => new SportListVm
                {
                    SportId = b.SportId,
                    SportName = b.SportName,
                    shoesQuantity = _shoesService.GetAll(filter: s => s.SportId == b.SportId).Count()
                }).ToList();

                var pagedList = sportsListVm.ToPagedList(currentPage, pageSize);

                return View(pagedList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        public IActionResult UpSert(int? id)
        {
            SportEditVm sportVm;

            if (id == null || id == 0)
            {
                sportVm = new SportEditVm();
            }
            else
            {
                try
                {
                    var sport = _sportService.Get(filter: c => c.SportId == id);
                    if (sport == null)
                    {
                        return NotFound();
                    }
                    sportVm = _mapper.Map<SportEditVm>(sport);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the record.");
                }
            }
            return View(sportVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpSert(SportEditVm sportVm)
        {
            if (!ModelState.IsValid)
            {
                return View(sportVm);
            }

            try
            {
                var sport = _mapper.Map<Sports>(sportVm);

                if (_sportService.Exist(sport))
                {
                    ModelState.AddModelError(string.Empty, "Record already exists");
                    return View(sportVm);
                }

                _sportService.Save(sport);
                TempData["success"] = "Record successfully added/edited";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while editing the record.");
                return View(sportVm);
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

            var sport = _sportService.Get(filter: c => c.SportId == id);
            if (sport == null)
            {
                return NotFound();
            }

            try
            {
                if (_sportService.ItsRelated(sport.SportId))
                {
                    return Json(new { success = false, message = "Related record... Delete denied!" });
                }

                _sportService.Delete(sport);
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

            var sport = _sportService.Get(filter: c => c.SportId == id);
            if (sport == null)
            {
                return NotFound();
            }

            var currentPage = page ?? 1;
            int pageSize = 10;
            var sportVm = _mapper.Map<SportDetailsVm>(sport);
            sportVm.ShoesQuantity = _shoesService.GetAll(filter: s => s.SportId == sportVm.SportId).Count();

            var shoes = _shoesService.GetAll(
                orderBy: o => o.OrderBy(p => p.Model),
                filter: p => p.SportId == sportVm.SportId,
                propertiesNames: "Sports");

            sportVm.Shoes = _mapper.Map<List<ShoeListVm>>(shoes).ToPagedList(currentPage, pageSize);

            return View(sportVm);
        }
    }
}

