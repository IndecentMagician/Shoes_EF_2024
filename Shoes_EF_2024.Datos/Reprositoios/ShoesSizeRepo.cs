using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shoes_EF_2024.Datos.Interfaces;
using Shoes_EF_2024.Entidades;

namespace Shoes_EF_2024.Datos.Repositorios
{
    public class ShoesSizeRepo : GenericRepository<ShoeSize>, IShoeSizeRepo
    {
        private readonly ShoesDbContext _db;

        public ShoesSizeRepo(ShoesDbContext db) : base(db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public bool Exist(int shoeid, int sizeid)
        {
            return _db.ShoeSizes.Any(ss => ss.ShoeId == shoeid && ss.SizeId == sizeid);
        }

        public List<ShoeSize> GetAllByShoeId(int shoeId)
        {
            return _db.ShoeSizes
                .Include(ss => ss.Size)
                .Where(ss => ss.ShoeId == shoeId)
                .ToList();
        }

        public List<ShoeSize> GetAllBySizeId(int sizeId)
        {
            return _db.ShoeSizes
                .Include(ss => ss.Shoe)
                .Where(ss => ss.SizeId == sizeId)
                .ToList();
        }
    }
}
