using Shoes_EF_2024.Entidades;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shoes_EF_2024.Servicios.Interfaces
{
    public interface IServiceGenres
    {
        IEnumerable<Genre> GetAll(Expression<Func<Genre, bool>>? filter = null,
        Func<IQueryable<Genre>, IOrderedQueryable<Genre>>? orderBy = null,
            string? propertiesNames = null);
        void Save(Genre genre);
        void Delete(Genre genre);
        Genre? Get(Expression<Func<Genre, bool>>? filter = null,
            string? propertiesNames = null,
            bool tracked = true);
        bool Exist(Genre genre);
        bool ItsRelated(int id);
    }
}
