using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shoes_EF_2024.Web.ViewModels.Shoes
{
    public class ShoeEditVm
    {
        public int ShoeId { get; set; }

        [DisplayName("Model")]
        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; } = null!;

        [DisplayName("Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = null!;

        [DisplayName("Quantity per Unit")]
        public double QuantityPerUnit { get; set; } 

        [DisplayName("Price")]
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal UnitPrice { get; set; }

        [DisplayName("Stock")]
        [Required(ErrorMessage = "Stock is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Stock must be a positive number")]
        public double Stock { get; set; }

        public bool Suspended { get; set; }

        [DisplayName("Brand")]
        [Required(ErrorMessage = "Brand is required")]
        public int BrandId { get; set; }
        public IEnumerable<SelectListItem> Brands { get; set; } = Enumerable.Empty<SelectListItem>();

        [DisplayName("Sport")]
        [Required(ErrorMessage = "Sport is required")]
        public int SportId { get; set; }
        public IEnumerable<SelectListItem> Sports { get; set; } = Enumerable.Empty<SelectListItem>();

        [DisplayName("Genre")]
        [Required(ErrorMessage = "Genre is required")]
        public int GenreId { get; set; }
        public IEnumerable<SelectListItem> Genres { get; set; } = Enumerable.Empty<SelectListItem>();

        [DisplayName("Color")]
        [Required(ErrorMessage = "Color is required")]
        public int ColorID { get; set; }
        public IEnumerable<SelectListItem> Colors { get; set; } = Enumerable.Empty<SelectListItem>();

        [DisplayName("Size")]
        [Required(ErrorMessage = "Size is required")]
        public int SizeId { get; set; }
        public IEnumerable<SelectListItem> Sizes { get; set; } = Enumerable.Empty<SelectListItem>();

        [DisplayName("Image URL")]
        public string? ImageUrl { get; set; }
    }
}
