using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoes_EF_2024.Entidades
{
    public class Shoes
    {
        [Key]
        public int ShoeId { get; set; }

        [Required]
        public int BrandId { get; set; }

        [Required]
        public int SportId { get; set; }

        [Required]
        public int GenreId { get; set; }

        [Required]
        public int ColorID { get; set; }

        [Required]
        [StringLength(150)]
        public string Model { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal UnitPrice { get; set; }

        [Required]
        public double Stock { get; set; }

        [StringLength(500)]
        public string? ImageUrl { get; set; }

        public bool Suspended { get; set; }

        public Brands Brand { get; set; }
        public Sports Sport { get; set; }
        public Genre Genre { get; set; }
        public Colors Color { get; set; }

        public ICollection<ShoeSize> ShoeSizes { get; set; } = new List<ShoeSize>();
    }
}


