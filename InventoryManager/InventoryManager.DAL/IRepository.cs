using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.DAL
{
    // Interface for generic repository

    interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);

        void AddNew(TEntity obj);

        void Update(TEntity obj);

        void Delete(TEntity obj);
    }
}
