using Shoes_EF_2024.Entidades;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Shoes_EF_2024.Datos.Interfaces
{
    public interface IShoeSizeRepo : IGenericRepository<ShoeSize>
    {
        bool Exist(int shoeId, int sizeId);
        List<ShoeSize> GetAllByShoeId(int shoeId);
        List<ShoeSize> GetAllBySizeId(int sizeId);
    }
}