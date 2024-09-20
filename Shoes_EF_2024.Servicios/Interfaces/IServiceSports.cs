using Shoes_EF_2024.Entidades;
using System.Linq.Expressions;

namespace Shoes_EF_2024.Servicios.Interfaces
{
    public interface IServiceSports
    {
        void Save(Sports _sp);
        void Delete(Sports _sp);
        bool Exist(Sports _sp);
        bool ItsRelated(int id);

        public IEnumerable<Sports> GetAll(Expression<Func<Sports, bool>>? filter = null,
                Func<IQueryable<Sports>, IOrderedQueryable<Sports>>? orderBy = null,
                string? propertiesNames = null);
        public Sports? Get(Expression<Func<Sports, bool>>? filter = null, string? propertiesNames = null, bool tracked = true);
    }
}