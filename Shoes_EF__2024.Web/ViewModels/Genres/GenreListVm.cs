using System.ComponentModel;
namespace Shoes_EF_2024.Web.ViewModels.Genres
{
    public class GenrelistVm
    {
        public int GenreId { get; set; }
        [DisplayName("Genre")]
        public string GenreName { get; set;}
        public int shoesQuantity { get; set; }
    }
}
