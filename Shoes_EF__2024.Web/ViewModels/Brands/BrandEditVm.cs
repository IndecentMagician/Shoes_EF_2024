using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Shoes_EF_2024.Web.ViewModels.Brands
{
    public class BrandEditVm
    {
        public int BrandId { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(150, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 3)]
        [DisplayName("Brand Name")]
        public string BrandName { get; set; } = null!;
    }
}
