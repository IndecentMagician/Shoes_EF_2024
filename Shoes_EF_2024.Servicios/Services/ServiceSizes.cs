using Shoes_EF_2024.Datos.Interfaces;
using Shoes_EF_2024.Datos.Repositorios;
using Shoes_EF_2024.Entidades;
using Shoes_EF_2024.Servicios.Interfaces;
using System.Drawing;
using System.Linq.Expressions;

namespace Shoes_EF_2024.Servicios.Servicios
{
    public class ServiceSizes : IServiceSizes
    {
        private readonly ISizeRepo _repo;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceSizes(ISizeRepo? repository, IUnitOfWork? unitOfWork)
        {
            _repo = repository ?? throw new ArgumentException("Dependencies not set");
            _unitOfWork = unitOfWork ?? throw new ArgumentException("Dependencies not set");
        }

        public void Delete(Sizes sizes)
        {
            try
            {
                _unitOfWork!.BeginTransaction();
                _repo!.Delete(sizes);
                _unitOfWork!.Commit();
            }
            catch (Exception)
            {
                _unitOfWork!.Rollback();
                throw;
            }
        }

        public bool Exist(Sizes size)
        {
            return _repo!.Exist(size);
        }

        public Sizes? Get(Expression<Func<Sizes, bool>>? filter = null, string? propertiesNames = null, bool tracked = true)
        {
            return _repo!.Get(filter, propertiesNames, tracked);
        }

        public IEnumerable<Sizes> GetAll(Expression<Func<Sizes, bool>>? filter = null, Func<IQueryable<Sizes>, IOrderedQueryable<Sizes>>? orderBy = null, string? propertiesNames = null)
        {
            return _repo!.GetAll(filter, orderBy, propertiesNames);
        }

        public bool ItsRelated(int id)
        {
            return _repo!.ItsRelated(id);
        }

        public void Save(Sizes size)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                if (size.SizeId == 0)
                {
                    _repo?.Add(size);
                }
                else
                {
                    _repo?.Update(size);
                }
                _unitOfWork?.Commit();
            }
            catch (Exception)
            {
                _unitOfWork?.Rollback();
                throw;
            }
        }
    }
}

