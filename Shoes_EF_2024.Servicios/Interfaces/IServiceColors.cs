using Shoes_EF_2024.Entidades;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shoes_EF_2024.Servicios.Interfaces
{
    public interface IServiceColors
    {
        void Save(Colors color);
        void Delete(Colors color);
        bool Exist(Colors color);
        bool ItsRelated(int id);

        public IEnumerable<Colors> GetAll(Expression<Func<Colors, bool>>? filter = null,
                Func<IQueryable<Colors>, IOrderedQueryable<Colors>>? orderBy = null,
                string? propertiesNames = null);
        public Colors? Get(Expression<Func<Colors, bool>>? filter = null, string? propertiesNames = null, bool tracked = true);
    }
}
