using Shoes_EF_2024.Entidades;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shoes_EF_2024.Servicios.Interfaces
{
    public interface IServiceBrands
    {
        void Save(Brands brand);
        void Delete(Brands brand);
        bool Exist(Brands brand);
        bool ItsRelated(int id);

        public IEnumerable<Brands> GetAll(Expression<Func<Brands, bool>>? filter = null,
                Func<IQueryable<Brands>, IOrderedQueryable<Brands>>? orderBy = null,
                string? propertiesNames = null);

        public Brands? Get(Expression<Func<Brands, bool>>? filter = null, string? propertiesNames = null, bool tracked = true);

    }
}
