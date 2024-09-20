using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shoes_EF_2024.Entidades;
using Shoes_EF_2024.Servicios.Interfaces;
using Shoes_EF_2024.Web.ViewModels.ShoeSizes;
using X.PagedList.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Shoes_EF_2024.Web.Controllers
{
    public class SizesController : Controller
    {
        private readonly IServiceSizes _sizesService;
        private readonly IMapper _mapper;

        public SizesController(IServiceSizes sizesService, IMapper mapper)
        {
            _sizesService = sizesService ?? throw new ApplicationException("Dependencies not set");
            _mapper = mapper ?? throw new ApplicationException("Dependencies not set");
        }

        public IActionResult Index(int? page, string? searchTerm = null, bool viewAll = false, int pageSize = 10)
        {
            int pageNumber = page ?? 1;
            ViewBag.currentPageSize = pageSize;

            IEnumerable<Sizes>? sizes;
            if (!viewAll)
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    sizes = _sizesService.GetAll(
                        orderBy: o => o.OrderBy(s => s.SizeNumber),
                        filter: s => s.SizeNumber.ToString().Contains(searchTerm));
                    ViewBag.currentSearchTerm = searchTerm;
                }
                else
                {
                    sizes = _sizesService.GetAll(orderBy: o => o.OrderBy(s => s.SizeNumber));
                }
            }
            else
            {
                sizes = _sizesService.GetAll(orderBy: o => o.OrderBy(s => s.SizeNumber));
            }

            var sizesVm = _mapper.Map<List<SizeListVm>>(sizes).ToPagedList(pageNumber, pageSize);
            return View(sizesVm);
        }

        public IActionResult Details(int id)
        {
            var size = _sizesService.Get(s => s.SizeId == id);
            if (size == null)
            {
                return NotFound();
            }
            var sizeVm = _mapper.Map<SizeEditVm>(size);
            return View(sizeVm);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SizeEditVm sizeVm)
        {
            if (!ModelState.IsValid)
            {
                return View(sizeVm);
            }

            try
            {
                var size = _mapper.Map<Sizes>(sizeVm);
                _sizesService.Save(size);
                TempData["success"] = "Size successfully added.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while adding the size.");
                return View(sizeVm);
            }
        }

        public IActionResult Edit(int id)
        {
            var size = _sizesService.Get(s => s.SizeId == id);
            if (size == null)
            {
                return NotFound();
            }
            var sizeVm = _mapper.Map<SizeEditVm>(size);
            return View(sizeVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SizeEditVm sizeVm)
        {
            if (!ModelState.IsValid)
            {
                return View(sizeVm);
            }

            try
            {
                var size = _mapper.Map<Sizes>(sizeVm);

                if (_sizesService.Exist(size))
                {
                    ModelState.AddModelError(string.Empty, "Size already exists.");
                    return View(sizeVm);
                }

                _sizesService.Save(size);
                TempData["success"] = "Size successfully updated.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating the size.");
                return View(sizeVm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var size = _sizesService.Get(s => s.SizeId == id);
            if (size == null)
            {
                return NotFound();
            }

            try
            {
                if (_sizesService.ItsRelated(size.SizeId))
                {
                    return Json(new { success = false, message = "Related record... Delete denied!" });
                }

                _sizesService.Delete(size);
                return Json(new { success = true, message = "Size successfully deleted." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Couldn't delete the size!" });
            }
        }
    }
}


