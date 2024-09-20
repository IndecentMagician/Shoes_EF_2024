using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Shoes_EF_2024.Web.ViewModels.Colors
{
    public class ColorEditVm
    {
        public int ColorId { get; set; }
        [StringLength(200, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 3)]
        [DisplayName("Color Name")]
        public string ColorName { get; set; } = string.Empty; 
    }
}
