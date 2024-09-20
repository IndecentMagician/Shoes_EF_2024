using Shoes_EF_2024.Web.ViewModels.Shoes;
using System.ComponentModel;
using X.PagedList;
namespace Shoes_EF_2024.Web.ViewModels.Sports
{
    public class SportDetailsVm
    {

        public int SportId { get; set; }
        [DisplayName("Sport")]
        public string SportName { get; set; } = null!;

        [DisplayName("Shoes Qty")]
        public int ShoesQuantity { get; set; }

        public IPagedList<ShoeListVm>? Shoes { get; set; }
    }
}
