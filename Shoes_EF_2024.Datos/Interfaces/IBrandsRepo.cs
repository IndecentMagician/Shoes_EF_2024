using Shoes_EF_2024.Datos.Repositorios;
using Shoes_EF_2024.Entidades;
using System.Threading.Tasks;


namespace Shoes_EF_2024.Datos.Interfaces
{
    public interface IBrandsRepo : IGenericRepository<Brands>  
    {

        //void Guardar(Brands g);
        //void Borrar(Brands g);
        //void Editar(Brands g);
        //public bool existe(Brands d);
        //bool estarelacionado(Brands b);
        //List<Shoes>? GetAll(Brands? b);
        void Update(Brands brand);
        bool Exist(Brands brand);
        bool ItsRelated(int id);


    }
}
