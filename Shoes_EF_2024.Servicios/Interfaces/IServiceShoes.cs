using Shoes_EF_2024.Entidades;
using Shoes_EF_2024.Entidades.Dto;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Shoes_EF_2024.Servicios.Interfaces
{
    public interface IServiceShoes
    {

        Shoes? Get(Expression<Func<Shoes, bool>>? filter = null, string? propertiesNames = null, bool tracked = true);

        IEnumerable<Shoes> GetAll(Expression<Func<Shoes, bool>>? filter = null, Func<IQueryable<Shoes>, IOrderedQueryable<Shoes>>? orderBy = null, string? propertiesNames = null);

        bool ItsRelated(int id);

        bool Exist(Shoes shoe);

        void Delete(Shoes shoe);

        void Save(Shoes shoe);
    }
}