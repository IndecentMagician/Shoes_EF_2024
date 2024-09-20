using Shoes_EF_2024.Web.ViewModels.Colors;
using System.ComponentModel;
using X.PagedList;

namespace Shoes_EF_2024.Web.ViewModels.Colors
{
    public class ColorlistVm
    {
        public int ColorId { get; set; }
        [DisplayName("Color")]
        public string ColorName { get; set; }
        public int shoesQuantity { get; set; }
    }
}
