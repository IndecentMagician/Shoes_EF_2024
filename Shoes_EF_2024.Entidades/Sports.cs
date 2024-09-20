using System.ComponentModel.DataAnnotations;

namespace Shoes_EF_2024.Entidades
{
    public class Sports
    {
        [Key]
        public int SportId { get; set; }
        [StringLength(20)]
        public string SportName { get; set; } = null!;
        public bool Active { get; set; }
    }
}
