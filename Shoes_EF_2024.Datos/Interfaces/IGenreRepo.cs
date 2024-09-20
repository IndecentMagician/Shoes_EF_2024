using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shoes_EF_2024.Entidades;

namespace Shoes_EF_2024.Datos.Interfaces
{
    public interface IGenreRepo : IGenericRepository<Genre>
    {



        void Editar(Genre g);
        public bool Exist(Genre d);
        bool ItsRelated(int id);
    }
}
