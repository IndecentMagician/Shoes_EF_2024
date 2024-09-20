using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shoes_EF_2024.Entidades;
using Shoes_EF_2024.Entidades.Dto;
using Shoes_EF_2024.Entidades.Enum;

namespace Shoes_EF_2024.Datos.Interfaces
{
    public interface IRepositoryShoe : IGenericRepository<Shoes>
    {
        void Update(Shoes shoe);
        bool Exist(Shoes shoe);
        bool ItsRelated(int id);


    }
}
