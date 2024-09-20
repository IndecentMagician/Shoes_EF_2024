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

namespace Shoes_EF_2024.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceShoes _shoeService;

        public HomeController(ILogger<HomeController> logger, IServiceShoes shoeService)
        {
            _shoeService = shoeService;
            _logger = logger;
            _shoeService=shoeService;
        }

        public IActionResult Index()
        {
            var shoes = _shoeService.GetAll(); 
            var topShoes = shoes.Take(5); 

            return View(topShoes);
            return View();
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
