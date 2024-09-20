using Shoes_EF_2024.Entidades;

namespace Shoes_EF_2024.Datos.Interfaces
{
    public interface IColorsRepo : IGenericRepository<Colors>
    {
        void Editar(Colors c);
        public bool Exist(Colors c);
        bool ItsRelated(int id);
    }
}
