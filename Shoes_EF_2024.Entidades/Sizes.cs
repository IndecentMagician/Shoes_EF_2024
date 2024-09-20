using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoes_EF_2024.Entidades
{
    public class Sizes
    {
        [Key]
        public int SizeId { get; set; }

        [Required]
        [Column(TypeName = "decimal(3, 1)")]
        public decimal SizeNumber { get; set; }

        public virtual ICollection<ShoeSize> ShoeSizes { get; set; } = new List<ShoeSize>();
    }
}
