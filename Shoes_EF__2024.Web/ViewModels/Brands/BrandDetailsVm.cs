using Shoes_EF_2024.Web.ViewModels.Shoes;
using System.ComponentModel;
using X.PagedList;
namespace Shoes_EF_2024.Web.ViewModels.Brands
{
    public class BrandDetailsVm
    {

        public int BrandId { get; set; }
        [DisplayName("Brand")]
        public string BrandName { get; set; } = null!;

        [DisplayName("Shoes Qty")]
        public int ShoesQuantity { get; set; }

        public IPagedList<ShoeListVm>? Shoes { get; set; }
    }
}
