using Shoes_EF_2024.Datos;
using Shoes_EF_2024.Datos.Interfaces;
using Shoes_EF_2024.Entidades;
using Shoes_EF_2024.Servicios.Interfaces;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Shoes_EF_2024.Servicios.Services
{
    public class ServiceColors : IServiceColors
    {
        private readonly ShoesDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IColorsRepo _repo;
        public ServiceColors(IColorsRepo? Repository, IUnitOfWork unitOfWork)
        {
            _repo = Repository ?? throw new ArgumentException("Dependencies not set");
            _unitOfWork = unitOfWork ?? throw new ArgumentException("Dependencies not set");
        }

        public IEnumerable<Colors> GetAll(Expression<Func<Colors, bool>>? filter = null,
                Func<IQueryable<Colors>, IOrderedQueryable<Colors>>? orderBy = null,
                string? propertiesNames = null)
        {
            return _repo!.GetAll(filter, orderBy, propertiesNames);
        }

        public Colors? Get(Expression<Func<Colors, bool>>? filter = null, string? propertiesNames = null, bool tracked = true)
        {
            return _repo!.Get(filter, propertiesNames, tracked);
        }

        public void Save(Colors color)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                if (color.ColorId==0)
                {
                    _repo?.Add(color);
                }
                else
                {
                    _repo?.Editar(color);
                }
                _unitOfWork?.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public void Delete(Colors color)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _repo.Delete(color);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public bool Exist(Colors color)
        {
            return _repo!.Exist(color);
        }

        public bool ItsRelated(int Id)
        {
            try
            {
                return _repo.ItsRelated(Id);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
