using System.ComponentModel;

namespace Shoes_EF_2024.Web.ViewModels.Shoes
{
    public class ShoeHomeIndexVm
    {
        public int ShoeId { get; set; }

        public string Model { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal ListPrice { get; set; }
        public decimal CashPrice { get; set; }
        public bool Suspended { get; set; }

        public string BrandName { get; set; } = null!;
        public string SportName { get; set; } = null!;
        public string GenreName { get; set; } = null!;
        public string ColorName { get; set; } = null!;

        public string? ImageUrl { get; set; }
    }
}
