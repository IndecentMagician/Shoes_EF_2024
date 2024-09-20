using System.ComponentModel.DataAnnotations;


namespace Shoes_EF_2024.Entidades
{
    public class Brands
    {
        [Key]
        public int BrandId { get; set; }
        [Required]
        [StringLength(100)]
        public string BrandName { get; set; } = null!;

        public bool Active { get; set; }   
    }
}
