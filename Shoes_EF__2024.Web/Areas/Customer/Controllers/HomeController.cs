using Microsoft.AspNetCore.Mvc;
using Shoes_EF_2024.Web.Models;
using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shoes_EF_2024.Entidades;
using Shoes_EF_2024.Servicios.Interfaces;
using Shoes_EF_2024.Web.ViewModels.Shoes;
using Shoes_EF_2024.Web.ViewModels.ShoeSizes;
using X.PagedList.Extensions;

namespace Shoes_EF_2024.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {

        private readonly IServiceShoes _shoeService;
        private readonly IMapper _mapper;

        public HomeController(IServiceShoes shoeService, IMapper mapper)
        {
            _shoeService=shoeService;
            _mapper=mapper;
        }

        public IActionResult Index()
        {
            var shoes = _shoeService.GetAll();
            var topShoes = shoes.Take(5);

            return View(topShoes);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
