using Shoes_EF_2024.Entidades;
using Microsoft.EntityFrameworkCore;
using Shoes_EF_2024.Datos.Interfaces;
using System.Diagnostics.Metrics;



namespace Shoes_EF_2024.Datos.Repositorios
{
    public class GenreRepo : GenericRepository<Genre>, IGenreRepo
    {
        private readonly ShoesDbContext _db;
        public GenreRepo(ShoesDbContext db) : base(db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void Editar(Genre g)
        {
            _db.Genres.Update(g);
        }

        public bool Exist(Genre d)
        {
            if (d.GenreId == 0)
            {
                return _db.Genres.Any(c => c.GenreName == d.GenreName);
            }
            return _db.Genres.Any(c => c.GenreName == d.GenreName && c.GenreId != d.GenreId);
        }

        public bool ItsRelated(int id)
        {
            return _db!.Shoes.Any(c => c.GenreId == id);
        }
    }
}
