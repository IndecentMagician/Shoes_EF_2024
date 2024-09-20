using Microsoft.EntityFrameworkCore;
using Shoes_EF_2024.Entidades;

namespace Shoes_EF_2024.Datos
{
    public partial class ShoesDbContext : DbContext
    {

        public ShoesDbContext(DbContextOptions<ShoesDbContext> options) : base(options)
        {
        }

        public DbSet<Brands> Brands { get; set; }
        public DbSet<Colors> Colors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Shoes> Shoes { get; set; }
        public DbSet<Sports> Sports { get; set; }
        public DbSet<Sizes> Sizes { get; set; }
        public DbSet<ShoeSize> ShoeSizes { get; set; }


    }
}
