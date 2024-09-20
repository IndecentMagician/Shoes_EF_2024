using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shoes_EF_2024.Web.ViewModels.ShoeSizes
{
    public class ShoeSizeEditVm
    {
        public int ShoeId { get; set; }
        public int SizeId { get; set; }
        public int QuantityInStock { get; set; }

        public List<SelectListItem>? Shoes { get; set; }
        public List<SelectListItem>? Sizes { get; set; }
    }
}