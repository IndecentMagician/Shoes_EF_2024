using Shoes_EF_2024.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shoes_EF_2024.Datos.Interfaces;

namespace Shoes_EF_2024.Datos.Repositorios
{
    //public class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
    public class BrandsRepo : GenericRepository<Brands>, IBrandsRepo
    {
        private readonly ShoesDbContext _db;

        public BrandsRepo(ShoesDbContext db) : base(db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public bool Exist(Brands brand)
        {

            if (brand.BrandId == 0)
            {
                return _db.Brands.Any(c => c.BrandName == brand.BrandName);
            }
            return _db.Brands.Any(p => p.BrandName == brand.BrandName && p.BrandId != brand.BrandId);
        }


        public bool ItsRelated(int id)
        {
            return _db.Brands.Any(p => p.BrandId == id);
        }

        public void Update(Brands brand)
        {

            _db.Brands.Update(brand);

        }

    }

}
