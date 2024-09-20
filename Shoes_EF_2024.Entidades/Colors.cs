using System.ComponentModel.DataAnnotations;

namespace Shoes_EF_2024.Entidades
{
    public class Colors
    {
        [Key]
        public int ColorId { get; set; }
        [Required]
        [MaxLength(50)]
        public string ColorName { get; set; } = null!;
        public bool Active { get; set; }
    }
}
