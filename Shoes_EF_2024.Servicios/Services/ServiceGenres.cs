using Shoes_EF_2024.Datos.Interfaces;
using Shoes_EF_2024.Entidades;
using Shoes_EF_2024.Servicios.Interfaces;
using System.Linq.Expressions;

namespace Shoes_EF_2024.Servicios.Services
{
    public class ServiceGenres : IServiceGenres
    {
        private readonly IGenreRepo _repo;
        private readonly IUnitOfWork _unitOfWork;


        public ServiceGenres(IGenreRepo? repository, IUnitOfWork? unitOfWork)
        {
            _repo = repository ?? throw new ArgumentException("Dependencies not set");
            _unitOfWork = unitOfWork ?? throw new ArgumentException("Dependencies not set");
        }

        public void Delete(Genre genre)
        {
            try
            {
                _unitOfWork!.BeginTransaction();
                _repo!.Delete(genre);
                _unitOfWork!.Commit();

            }
            catch (Exception)
            {
                _unitOfWork!.Rollback();
                throw;
            }
        }

        public bool Exist(Genre genre)
        {
            return _repo!.Exist(genre);
        }

        public Genre? Get(Expression<Func<Genre, bool>>? filter = null, string? propertiesNames = null, bool tracked = true)
        {
            return _repo!.Get(filter, propertiesNames, tracked);
        }

        public IEnumerable<Genre> GetAll(Expression<Func<Genre, bool>>? filter = null, Func<IQueryable<Genre>, IOrderedQueryable<Genre>>? orderBy = null, string? propertiesNames = null)
        {
            return _repo!.GetAll(filter, orderBy, propertiesNames);
        }

        public bool ItsRelated(int id)
        {
            return _repo!.ItsRelated(id);
        }

        public void Save(Genre genre)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                if (genre.GenreId == 0)
                {
                    _repo?.Add(genre);
                }
                else
                {
                    _repo?.Editar(genre);
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

