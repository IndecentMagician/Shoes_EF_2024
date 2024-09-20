using Shoes_EF_2024.Datos.Interfaces;
using Shoes_EF_2024.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq;




namespace Shoes_EF_2024.Datos.Repositorios
{
    public class RepositoryShoe : GenericRepository<Shoes>, IRepositoryShoe
    {
        private readonly ShoesDbContext _db;

        public RepositoryShoe(ShoesDbContext db) : base(db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void Update(Shoes shoe)
        {
            if (shoe.BrandId != 0)
            {
                _db.Attach(new Brands { BrandId = shoe.BrandId });
            }

            if (shoe.GenreId != 0)
            {
                _db.Attach(new Genre { GenreId = shoe.GenreId });
            }

            if (shoe.ColorID != 0)
            {
                _db.Attach(new Colors { ColorId = shoe.ColorID });
            }

            if (shoe.SportId != 0)
            {
                _db.Attach(new Sports { SportId = shoe.SportId });
            }

            base.Add(shoe); 
        }

        public bool ItsRelated(int shoeId)
        {
            
            return _db.ShoeSizes.Any(ss => ss.ShoeId == shoeId);
        }

        public bool Exist(Shoes shoe)
        {
            return _db.Shoes.Any(s =>
                s.Model == shoe.Model &&
                s.BrandId == shoe.BrandId &&
                s.GenreId == shoe.GenreId &&
                s.SportId == shoe.SportId &&
                (shoe.ShoeId == 0 || s.ShoeId != shoe.ShoeId));
        }
    }
}
