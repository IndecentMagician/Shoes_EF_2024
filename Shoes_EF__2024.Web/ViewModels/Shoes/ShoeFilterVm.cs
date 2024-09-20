using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shoes_EF_2024.Web.ViewModels.Brands;
using Shoes_EF_2024.Web.ViewModels.Genres;
using Shoes_EF_2024.Web.ViewModels.Shoes;
using Shoes_EF_2024.Web.ViewModels.Sports;
using Shoes_EF_2024.Web.ViewModels.Colors;

namespace Shoes_EF_2024.Web.ViewModels.Shoes
{
    public class ShoeFilterVm
    {
        public IPagedList<ShoeListVm> Shoes { get; set; }

        public List<SelectListItem> Brands { get; set; }
        public List<SelectListItem> Sports { get; set; }
        public List<SelectListItem> Genres { get; set; }
        public List<SelectListItem> Colors { get; set; }

        public int? FilterBrandId { get; set; }
        public int? FilterSportId { get; set; }
        public int? FilterGenreId { get; set; }
        public int? FilterColorId { get; set; }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool ViewAll { get; set; }
    }
}
