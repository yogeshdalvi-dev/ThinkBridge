using InventoryManager.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.DAL
{
    class UnitOfWork
    {
        private InventoryManagementEntities2 _dbContext = new InventoryManagementEntities2();

        // Get repository instance 
        public Repository.Repository<TEntityType> GetRepoInstance<TEntityType>() where TEntityType : class
        {
            return new Repository.Repository<TEntityType>(_dbContext);
        }


        // save changes at once
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
