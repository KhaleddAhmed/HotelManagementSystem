using HotelManagement.Core.Entities.Hotel;
using HotelManagement.Core.Repositories.Contract;
using HotelManagement.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Repository.Repositories
{
    public class GenericRepository<TEntity,TKey>:IGenericRepository<TEntity,TKey> where TEntity : class
    {
        private readonly HotelDbContext _dbContext;

        public GenericRepository(HotelDbContext dbContext) // ASK CLR For Creating Object From StoreContext Implicitly
        {
            _dbContext = dbContext;
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
    }
}
