using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shoes_EF_2024.Web.ViewModels.Genres
{
    public class GenreEditVm
    {
        public int GenreId {  get; set; }
        [StringLength(200, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 3)]
        [DisplayName("Genre Name")]
        public string GenreName { get; set; } = null!;
    }
}
