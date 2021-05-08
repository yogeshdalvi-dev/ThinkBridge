
using InventoryManager.DAL.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.DAL.Repository
{
    // Generice repository for all the entities
    // set the DbContext and dbSet from this class 

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> _dbSet;
        private readonly InventoryManagementEntities2 _dbContext;
        public Repository(InventoryManagementEntities2 dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public void AddNew(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }
    }
}
