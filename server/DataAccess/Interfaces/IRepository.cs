using DataAccess.Models;

namespace DataAccess.Interfaces;


    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        void Create(TEntity item);
        void Update(TEntity item);
        void Delete(int id);
    }
    