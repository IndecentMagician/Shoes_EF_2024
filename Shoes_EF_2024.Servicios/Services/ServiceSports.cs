using Shoes_EF_2024.Datos;
using Shoes_EF_2024.Datos.Interfaces;
using Shoes_EF_2024.Entidades;
using Shoes_EF_2024.Servicios.Interfaces;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace Shoes_EF_2024.Servicios.Services
{
    public class ServiceSports : IServiceSports
    {
        private readonly ShoesDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        private readonly ISportsRepo _repo;
        public ServiceSports(ISportsRepo? Repository, IUnitOfWork unitOfWork)
        {
            _repo = Repository ?? throw new ArgumentException("Dependencies not set");
            _unitOfWork = unitOfWork ?? throw new ArgumentException("Dependencies not set");
        }

        public IEnumerable<Sports> GetAll(Expression<Func<Sports, bool>>? filter = null,
                Func<IQueryable<Sports>, IOrderedQueryable<Sports>>? orderBy = null,
                string? propertiesNames = null)
        {
            return _repo!.GetAll(filter, orderBy, propertiesNames);
        }

        public Sports? Get(Expression<Func<Sports, bool>>? filter = null, string? propertiesNames = null, bool tracked = true)
        {
            return _repo!.Get(filter, propertiesNames, tracked);
        }

        public void Save(Sports _sport)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                if (_sport.SportId==0)
                {
                    _repo?.Add(_sport);
                }
                else
                {
                    _repo?.Editar(_sport);
                }
                _unitOfWork?.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public void Delete(Sports _sport)
        {
            var SportsInDb = _context.Sports.Find(_sport.SportId);
            if (SportsInDb != null)
            {
                _context.Sports.Remove(SportsInDb);
                _context.SaveChanges();
                Console.WriteLine($"Deporte {SportsInDb.SportName} Eliminado");
            }
            else
            {
                Console.WriteLine("Deporte no existe");
            }
        }

        public bool Exist(Sports sport)
        {
            return _repo!.Exist(sport);
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
