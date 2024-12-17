using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Repositories.Contract
{
    public interface IGenericRepository<TEntity,TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetAsync(TKey id);
        Task AddAsync(TEntity entity);


        void Update(TEntity entity);
        void Delete(TEntity entity);

    }
}
