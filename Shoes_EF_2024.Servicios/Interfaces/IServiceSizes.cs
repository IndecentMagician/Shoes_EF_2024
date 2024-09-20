using Shoes_EF_2024.Entidades;
using System.Linq.Expressions;

namespace Shoes_EF_2024.Servicios.Interfaces
{
    public interface IServiceSizes
    {
        void Delete(Sizes size);
        bool Exist(Sizes size);
        Sizes? Get(Expression<Func<Sizes, bool>>? filter = null, string? propertiesNames = null, bool tracked = true);
        IEnumerable<Sizes> GetAll(Expression<Func<Sizes, bool>>? filter = null, Func<IQueryable<Sizes>, IOrderedQueryable<Sizes>>? orderBy = null, string? propertiesNames = null);
        bool ItsRelated(int id);
        void Save(Sizes size);
    }
}
