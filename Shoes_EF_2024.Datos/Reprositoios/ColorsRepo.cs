using Shoes_EF_2024.Entidades;
using System.Threading.Tasks;
using Shoes_EF_2024.Datos.Interfaces;

namespace Shoes_EF_2024.Datos.Repositorios
{
    public class ColorsRepo : GenericRepository<Colors>, IColorsRepo
    {
        private readonly ShoesDbContext _db;


        public ColorsRepo(ShoesDbContext db) : base(db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void Editar(Colors c)
        {
            _db.Colors.Update(c);
        }

        public bool ItsRelated(int id)
        {
            return _db!.Shoes.Any(c => c.ColorID == id);
        }

        public bool Exist(Colors c)
        {
            if (c.ColorId == 0)
            {
                return _db.Colors.Any(existing => existing.ColorName == c.ColorName);
            }
            return _db.Colors.Any(existing => existing.ColorName == c.ColorName && existing.ColorId != c.ColorId);
        }

    }
}