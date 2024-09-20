using System.ComponentModel;

namespace Shoes_EF_2024.Web.ViewModels.ShoeSizes
{
    public class SizeListVm
    {
        public int SizeId { get; set; }

        [DisplayName("Size Number")]
        public string SizeNumber { get; set; } = null!;
    }
}