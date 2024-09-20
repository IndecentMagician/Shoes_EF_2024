using System.ComponentModel.DataAnnotations;

namespace Shoes_EF_2024.Entidades
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        [Required]
        [MaxLength(50)]
        public string GenreName { get; set; } = null!;
    }
}
