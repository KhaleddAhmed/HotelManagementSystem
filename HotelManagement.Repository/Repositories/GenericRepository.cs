using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.Entities.Hotel;
using HotelManagement.Core.Repositories.Contract;
using HotelManagement.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace HotelManagement.Repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : class
    {
        private readonly HotelDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(HotelDbContext dbContext) // ASK CLR For Creating Object From StoreContext Implicitly
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(TKey id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Remove(entity);
        }

        public async Task<IQueryable<TEntity>> GetAllAsyncAsQueryable() => _dbSet.AsNoTracking();

        public async Task<IQueryable<TEntity>> Get(Expression<Func<TEntity, bool>> predict = null)
        {
            return _dbSet.Where(predict);
        }
    }
}
