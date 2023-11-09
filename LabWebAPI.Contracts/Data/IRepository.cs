using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWebAPI.Contracts.Data
{
    public interface IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByKeyAsync<TKey>(TKey key);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<int> SaveChangesAsync();
    }
}
