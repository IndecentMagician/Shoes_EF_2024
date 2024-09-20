using Shoes_EF_2024.Entidades;
using Microsoft.EntityFrameworkCore;
using Shoes_EF_2024.Datos.Interfaces;



namespace Shoes_EF_2024.Datos.Repositorios
{
    public class SizesRepo : GenericRepository<Sizes>, ISizeRepo
    {
        private readonly ShoesDbContext _db;

        public SizesRepo(ShoesDbContext db) : base(db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public bool Exist(Sizes size)
        {
            if (size.SizeId == 0)
            {
                return _db.Sizes.Any(s => s.SizeNumber == size.SizeNumber);
            }
            return _db.Sizes.Any(s => s.SizeNumber == size.SizeNumber && s.SizeId != size.SizeId);
        }

        public bool ItsRelated(int id)
        {
            return _db.Sizes.Any(s => s.SizeId == id);
        }

        public void Update(Sizes size)
        {
            _db.Sizes.Update(size);
        }
    }
}
