using Shoes_EF_2024.Entidades;
using System.Threading.Tasks;
using Shoes_EF_2024.Datos.Interfaces;

namespace Shoes_EF_2024.Datos.Repositorios
{
    public class SportsRepo : GenericRepository<Sports>, ISportsRepo
    {
        private readonly ShoesDbContext _db;


        public SportsRepo(ShoesDbContext db) : base(db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void Editar(Sports c)
        {
            _db.Sports.Update(c);
        }

        public bool ItsRelated(int id)
        {
            return _db!.Shoes.Any(c => c.SportId == id);
        }

        public bool Exist(Sports sp)
        {
            if (sp.SportId == 0)
            {
                return _db.Sports.Any(existing => existing.SportName == sp.SportName);
            }
            return _db.Sports.Any(existing => existing.SportName == sp.SportName && existing.SportId != sp.SportId);
        }

    }
}