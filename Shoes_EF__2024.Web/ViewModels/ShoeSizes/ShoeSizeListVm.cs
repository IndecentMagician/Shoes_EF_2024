using System.ComponentModel;


namespace Shoes_EF_2024.Web.ViewModels.ShoeSizes
{
    public class ShoeSizeListVm
    {
        public int ShoeId { get; set; }
        public string ShoeModel { get; set; }
        public int SizeId { get; set; }
        public string SizeNumber { get; set; }
        public int QuantityInStock { get; set; }

        public string ShoeDisplay => $"{ShoeModel} - {SizeNumber}";

    }
}