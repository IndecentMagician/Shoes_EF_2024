using Shoes_EF_2024.Web.ViewModels.Shoes;
using System.ComponentModel;
using X.PagedList;
namespace Shoes_EF_2024.Web.ViewModels.Genres
{
    public class GenreDetailsVm
    {

        public int GenreId { get; set; }
        [DisplayName("Genre")]
        public string GenreName { get; set; } = null!;

        [DisplayName("Shoes Qty")]
        public int ShoesQuantity { get; set; }

        public IPagedList<ShoeListVm>? Shoes { get; set; }
    }
}
