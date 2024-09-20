using Shoes_EF_2024.Web.ViewModels.Shoes;
using System.ComponentModel;
using X.PagedList;

namespace Shoes_EF_2024.Web.ViewModels.Brands
{
    public class BrandListVm
    {
        public IPagedList<BrandListVm> Brands { get; set; }
        public int BrandId { get; set; }
        [DisplayName("Brand")]
        public string BrandName { get; set; } = null!;
        [DisplayName("Shoes. Qty")]
        public int shoesQuantity { get; set; }

    }
}
