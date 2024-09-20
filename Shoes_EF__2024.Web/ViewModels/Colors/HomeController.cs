using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shoes_EF_2024.Web.ViewModels.Colors
{
    public class ColorFilterVm
    {
        public IPagedList<ColorlistVm> Colors { get; set; }

        public List<SelectListItem> Brands { get; set; }
        public List<SelectListItem> Shoes { get; set; }

        public int? FilterBrandId { get; set; }
        public int? FilterShoeId { get; set; }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool ViewAll { get; set; }
    }
}
