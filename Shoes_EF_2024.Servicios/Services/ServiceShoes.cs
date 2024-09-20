using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Shoes_EF_2024.Datos.Interfaces;
using Shoes_EF_2024.Entidades;
using Shoes_EF_2024.Servicios.Interfaces;
using Shoes_EF_2024.Datos;

namespace Shoes_EF_2024.Servicios.Servicios
{
    public class ServiceShoes : IServiceShoes
    {
        private readonly IRepositoryShoe _repo;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceShoes(IRepositoryShoe repo, IUnitOfWork unitOfWork)
        {
            _repo = repo ?? throw new ArgumentException("Repository not set");
            _unitOfWork = unitOfWork ?? throw new ArgumentException("UnitOfWork not set");
        }

        public Shoes? Get(Expression<Func<Shoes, bool>>? filter = null, string? includeProperties = "Brand,Sport,Genre,Color", bool tracked = true)
        {
            return _repo.Get(filter, includeProperties, tracked);
        }

        public IEnumerable<Shoes> GetAll(Expression<Func<Shoes, bool>>? filter = null,
            Func<IQueryable<Shoes>, IOrderedQueryable<Shoes>>? orderBy = null,
            string? includeProperties = "Brand,Sport,Genre,Color")
        {
            return _repo.GetAll(filter, orderBy, includeProperties);
        }

        public bool ItsRelated(int id)
        {
            return _repo.ItsRelated(id);
        }

        public bool Exist(Shoes shoe)
        {
            return _repo.Exist(shoe);
        }

        public void Delete(Shoes shoe)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _repo.Delete(shoe);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public void Save(Shoes shoe)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                if (shoe.ShoeId == 0)
                {
                    _repo.Add(shoe);
                }
                else
                {
                    _repo.Update(shoe);
                }
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
    }
}
