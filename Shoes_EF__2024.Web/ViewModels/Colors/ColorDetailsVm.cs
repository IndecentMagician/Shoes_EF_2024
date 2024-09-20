using Shoes_EF_2024.Web.ViewModels.Shoes;
using System.ComponentModel;
using X.PagedList;
namespace Shoes_EF_2024.Web.ViewModels.Colors
{
    public class ColorDetailsVm
    {
        public int ColorId { get; set; }
        [DisplayName("Color")]
        public string ColorName { get; set; } = null!;

        [DisplayName("Shoes Qty")]
        public int ShoesQuantity { get; set; }

        public IPagedList<ShoeListVm>? Shoes { get; set; }
    }
}
