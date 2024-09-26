using Shoes_EF_2024.Datos;
using Shoes_EF_2024.Datos.Interfaces;
using Shoes_EF_2024.Entidades;
using Shoes_EF_2024.Servicios.Interfaces;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shoes_EF_2024.Servicios.Services
{
    public class ServiceBrands : IServiceBrands
    {
        private readonly ShoesDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IBrandsRepo _repo;
        public ServiceBrands(IBrandsRepo? Repository, IUnitOfWork unitOfWork)
        {
            _repo = Repository ?? throw new ArgumentException("Dependencies not set");
            _unitOfWork = unitOfWork ?? throw new ArgumentException("Dependencies not set");
        }

        public IEnumerable<Brands> GetAll(Expression<Func<Brands, bool>>? filter = null,
                Func<IQueryable<Brands>, IOrderedQueryable<Brands>>? orderBy = null,
                string? propertiesNames = null)
                    {
                            return _repo!.GetAll(filter, orderBy, propertiesNames);
                    }

        public Brands? Get(Expression<Func<Brands, bool>>? filter = null, string? propertiesNames = null, bool tracked = true)
        {
            return _repo!.Get(filter, propertiesNames, tracked);
        }

        public void Save(Brands brands)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                if (brands.BrandId==0)
                {
                    _repo?.Add(brands);
                }
                else
                {
                    _repo?.Update(brands);
                }
                _unitOfWork?.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public void Delete(Brands brand)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _repo.Delete(brand);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public bool Exist(Brands brand)
        {
            return _repo!.Exist(brand);
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
