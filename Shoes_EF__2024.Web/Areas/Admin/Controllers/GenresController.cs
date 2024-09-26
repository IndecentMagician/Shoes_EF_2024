using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Shoes_EF_2024.Entidades;
using Shoes_EF_2024.Servicios.Interfaces;
using X.PagedList;
using Shoes_EF_2024.Web.ViewModels.Shoes;
using Microsoft.AspNetCore.Http;
using X.PagedList.Extensions;
using Shoes_EF_2024.Web.ViewModels.Genres;

namespace Shoes_EF_2024.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenresController : Controller
    {
        private readonly IServiceGenres _genresService;
        private readonly IServiceShoes _shoesService;
        private readonly IMapper _mapper;

        public GenresController(IServiceGenres genresService, IServiceShoes shoesService, IMapper mapper)
        {
            _genresService = genresService ?? throw new ApplicationException("Dependencies not set");
            _shoesService = shoesService ?? throw new ApplicationException("Dependencies not set");
            _mapper = mapper ?? throw new ApplicationException("Dependencies not set");
        }

        public IActionResult Index(int? page, string? searchTerm = null, int pageSize = 10)
        {
            try
            {
                var currentPage = page ?? 1;

                var genres = _genresService.GetAll();

                var genresListVm = genres.Select(b => new GenrelistVm
                {
                    GenreId = b.GenreId,
                    GenreName = b.GenreName,
                    shoesQuantity = _shoesService.GetAll(filter: s => s.GenreId == b.GenreId).Count()
                }).ToList();

                var pagedList = genresListVm.ToPagedList(currentPage, pageSize);

                return View(pagedList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        public IActionResult UpSert(int? id)
        {
            GenreEditVm genreVm;

            if (id == null || id == 0)
            {
                genreVm = new GenreEditVm();
            }
            else
            {
                try
                {
                    var genres = _genresService.Get(filter: c => c.GenreId == id);
                    if (genres == null)
                    {
                        return NotFound();
                    }
                    genreVm = _mapper.Map<GenreEditVm>(genres);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the record.");
                }
            }
            return View(genreVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpSert(GenreEditVm genreVm)
        {
            if (!ModelState.IsValid)
            {
                return View(genreVm);
            }

            try
            {
                var genre = _mapper.Map<Genre>(genreVm);

                if (_genresService.Exist(genre))
                {
                    ModelState.AddModelError(string.Empty, "Record already exists");
                    return View(genreVm);
                }

                _genresService.Save(genre);
                TempData["success"] = "Record successfully added/edited";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while editing the record.");
                return View(genreVm);
            }
        }

        [HttpPost]
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            Genre? _genre = _genresService?.Get(filter: s => s.GenreId == id);
            if (_genre is null)
            {
                return NotFound();
            }
            try
            {
                if (_genresService!.ItsRelated(_genre.GenreId))
                {
                    return Json(new { success = false, message = "Related Record... Delete Deny!!" }); ;
                }
                _genresService.Delete(_genre);
                return Json(new { success = true, message = "Record successfully deleted" });
            }
            catch (Exception)
            {

                return Json(new { success = false, message = "Couldn't delete record!!! " }); ;

            }
        }

        public IActionResult Details(int? id, int? page)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var genre = _genresService.Get(filter: c => c.GenreId == id);
            if (genre == null)
            {
                return NotFound();
            }

            var currentPage = page ?? 1;
            int pageSize = 10;
            var genreVm = _mapper.Map<GenreDetailsVm>(genre);
            genreVm.ShoesQuantity = _shoesService.GetAll(filter: s => s.GenreId == genreVm.GenreId).Count();

            var shoes = _shoesService.GetAll(
                orderBy: o => o.OrderBy(p => p.Model),
                filter: p => p.GenreId == genreVm.GenreId,
                propertiesNames: "Sports");

            genreVm.Shoes = _mapper.Map<List<ShoeListVm>>(shoes).ToPagedList(currentPage, pageSize);

            return View(genreVm);
        }
    }
}

