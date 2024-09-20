using Shoes_EF_2024.Entidades;

namespace Shoes_EF_2024.Datos.Interfaces
{
    public interface ISportsRepo : IGenericRepository<Sports>
    {
        void Editar(Sports sport);
        bool ItsRelated(int id);
        bool Exist(Sports sport);
    }
}
