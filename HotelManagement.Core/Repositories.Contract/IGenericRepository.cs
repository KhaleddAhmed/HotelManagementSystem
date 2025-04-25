using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Repositories.Contract
{
    public interface IGenericRepository<TEntity, TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetAsync(TKey id);
        Task AddAsync(TEntity entity);
        Task<IQueryable<TEntity>> GetAllAsyncAsQueryable();
        Task<IQueryable<TEntity>> Get(Expression<Func<TEntity, bool>> predict = null);

        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
