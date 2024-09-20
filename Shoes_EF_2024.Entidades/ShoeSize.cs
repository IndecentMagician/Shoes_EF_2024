using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoes_EF_2024.Entidades
{
    public class ShoeSize
    {
        public ShoeSize(int shoeId, int sizeId)
        {
            ShoeId=shoeId;
            SizeId=sizeId;
        }

        [Key]
        public int ShoeSizeId { get; set; }

        [Required]
        public int ShoeId { get; set; }

        [Required]
        public int SizeId { get; set; }

        [Required]
        public int QuantityInStock { get; set; }

        public virtual Shoes Shoe { get; set; } = null!;
        public virtual Sizes Size { get; set; } = null!;
    }
}
