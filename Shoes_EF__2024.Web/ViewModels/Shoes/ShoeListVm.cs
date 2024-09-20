using System.ComponentModel;

namespace Shoes_EF_2024.Web.ViewModels.Shoes
{
    public class ShoeListVm
    {
        public int ShoeId { get; set; }

        [DisplayName("Model")]
        public string Model { get; set; } = null!;

        [DisplayName("Description")]
        public string Description { get; set; } = null!;

        [DisplayName("Quantity per Unit")]
        public double QuantityPerUnit { get; set; } 

        [DisplayName("Price")]
        public decimal UnitPrice { get; set; }

        [DisplayName("Stock")]
        public double Stock { get; set; }

        public bool Suspended { get; set; }

        [DisplayName("Brand")]
        public string BrandName { get; set; } = null!;

        [DisplayName("Sport")]
        public string SportName { get; set; } = null!;

        [DisplayName("Genre")]
        public string GenreName { get; set; } = null!;

        [DisplayName("Color")]
        public string ColorName { get; set; } = null!;

        public string? ImageUrl { get; set; }
    }
}