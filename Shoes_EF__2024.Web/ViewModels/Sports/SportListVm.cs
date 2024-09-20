using Shoes_EF_2024.Web.ViewModels.Shoes;
using System.ComponentModel;
using X.PagedList;

namespace Shoes_EF_2024.Web.ViewModels.Sports
{
    public class SportListVm
    {
        public IPagedList<SportListVm> sports { get; set; }
        public int SportId { get; set; }

        [DisplayName("Sport")]
        public string SportName { get; set; } = string.Empty;

        public int shoesQuantity { get; set; }
    }
}
