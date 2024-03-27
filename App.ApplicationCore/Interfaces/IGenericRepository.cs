using App.ApplicationCore.Common;
using App.ApplicationCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity 
    {
        Task<IEnumerable<TEntity>> GetAllAsync(QueryOptions queryOptions);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(Guid entityId);
        Task<TEntity> UpdateAsync(Guid entityId, TEntity entity);
        Task<bool> DeleteByIdAsync(Guid entityId);

    }
}
