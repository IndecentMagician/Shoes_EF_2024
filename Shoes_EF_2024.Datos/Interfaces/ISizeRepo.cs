using Shoes_EF_2024.Entidades;

namespace Shoes_EF_2024.Datos.Interfaces
{
    public interface ISizeRepo : IGenericRepository<Sizes>
    {
        bool Exist(Sizes size);
        bool ItsRelated(int id);
        void Update(Sizes size);
    }
}
