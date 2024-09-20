using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes_EF_2024.Entidades.Dto
{
    public class ShoesDto
    {
        public int ShoeId { get; set; }
        public string Brand { get; set; } = null!;
        public string Sport { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
