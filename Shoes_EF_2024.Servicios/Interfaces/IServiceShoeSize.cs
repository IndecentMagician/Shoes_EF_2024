using Shoes_EF_2024.Entidades;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Shoes_EF_2024.Servicios.Interfaces
{
    public interface IServiceShoeSize
    {
        void Save(int shoeId, int sizeId, int quantityInStock);
        void UpdateQuantity(int shoeId, int sizeId, int quantityInStock);
        bool Exists(int shoeId, int sizeId);
        List<ShoeSize> GetAllByShoeId(int shoeId);
        List<ShoeSize> GetAllBySizeId(int sizeId);
        void Delete(int shoeId, int sizeId);
        IEnumerable<ShoeSize> GetAll(Expression<Func<ShoeSize, bool>>? filter = null,
                                     Func<IQueryable<ShoeSize>, IOrderedQueryable<ShoeSize>>? orderBy = null,
                                     string? propertiesNames = null);
    }
}