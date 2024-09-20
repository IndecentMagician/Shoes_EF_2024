using Shoes_EF_2024.Entidades;
using Shoes_EF_2024.Servicios.Interfaces;
using Shoes_EF_2024.Datos.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Shoes_EF_2024.Servicios
{
    public class ServiceShoeSize : IServiceShoeSize
    {
        private readonly IShoeSizeRepo _repo;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceShoeSize(IShoeSizeRepo repo, IUnitOfWork unitOfWork)
        {
            _repo = repo ?? throw new ArgumentException("Repository not set");
            _unitOfWork = unitOfWork ?? throw new ArgumentException("UnitOfWork not set");
        }

        public void Save(int shoeId, int sizeId, int quantityInStock)
        {
            var shoeSize = new ShoeSize(shoeId,sizeId)
            {
                QuantityInStock = quantityInStock
            };

            _repo.Add(shoeSize);
            _unitOfWork.SaveChanges();
        }

        public void Delete(ShoeSize shoeSize)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _repo.Delete(shoeSize);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public bool Exists(int shoeId, int sizeId)
        {
            return _repo.Exist(shoeId, sizeId);
        }

        public void UpdateQuantity(int shoeId, int sizeId, int quantityInStock)
        {
            var shoeSize = _repo.Get(ss => ss.ShoeId == shoeId && ss.SizeId == sizeId);
            if (shoeSize != null)
            {
                shoeSize.QuantityInStock = quantityInStock;
                _repo!.Add(shoeSize);
                _unitOfWork.SaveChanges();
            }
        }

        public List<ShoeSize> GetAllByShoeId(int shoeId)
        {
            return _repo.GetAllByShoeId(shoeId);
        }

        public List<ShoeSize> GetAllBySizeId(int sizeId)
        {
            return _repo.GetAllBySizeId(sizeId);
        }

        public void Delete(int shoeId, int sizeId)
        {
            var entry = _repo.Get(ss => ss.ShoeId == shoeId && ss.SizeId == sizeId);
            if (entry != null)
            {
                _repo.Delete(entry);
                _unitOfWork.SaveChanges();
            }
        }

        public IEnumerable<ShoeSize> GetAll(Expression<Func<ShoeSize, bool>>? filter = null,
                                            Func<IQueryable<ShoeSize>, IOrderedQueryable<ShoeSize>>? orderBy = null,
                                            string? propertiesNames = null)
        {
            return _repo.GetAll(filter, orderBy, propertiesNames);
        }
    }
}

