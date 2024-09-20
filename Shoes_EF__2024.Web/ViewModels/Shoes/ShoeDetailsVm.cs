using Shoes_EF_2024.Entidades;
using Shoes_EF_2024.Web.ViewModels.ShoeSizes;
using System.Collections.Generic;
using System.ComponentModel;

namespace Shoes_EF_2024.Web.ViewModels.Shoes
{
    public class ShoeDetailsVm
    {
        public int ShoeId { get; set; }

        [DisplayName("Model")]
        public string Model { get; set; } = null!;

        [DisplayName("Description")]
        public string Description { get; set; } = null!;

        [DisplayName("Q. Unit")]
        public string? QuantityPerUnit { get; set; }

        [DisplayName("Price")]
        public decimal UnitPrice { get; set; }

        [DisplayName("Stock")]
        public double Stock { get; set; }

        [DisplayName("Suspended")]
        public bool Suspended { get; set; }

        [DisplayName("Brand")]
        public string BrandName { get; set; } = null!;

        [DisplayName("Sport")]
        public string SportName { get; set; } = null!;

        [DisplayName("Genre")]
        public string GenreName { get; set; } = null!;

        [DisplayName("Color")]
        public string ColorName { get; set; } = null!;

        [DisplayName("Size")]
        public string? SizeNumber { get; set; }

        [DisplayName("Image URL")]
        public string? ImageUrl { get; set; }

        [DisplayName("Shoe Sizes")]
        public List<ShoeSizeListVm>? ShoeSizes { get; set; } 

        [DisplayName("Number of Sizes")]
        public int NumberOfSizes { get; set; } 
    }
}
