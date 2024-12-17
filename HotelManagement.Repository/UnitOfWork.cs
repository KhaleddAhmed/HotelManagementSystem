using HotelManagement.Core;
using HotelManagement.Core.Repositories.Contract;
using HotelManagement.Repository.Data.Contexts;
using HotelManagement.Repository.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly HotelDbContext _dbContext;
        private Hashtable _repositories;

        public UnitOfWork(HotelDbContext dbContext) // ASK CLR For Creating ObjectFrom DbContext Implicitly
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }
        public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();


        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : class
        {
            var Key = typeof(TEntity).Name;



            if (!_repositories.ContainsKey(Key))
            {
                var repository = new GenericRepository<TEntity, TKey>(_dbContext);
                _repositories.Add(Key, repository);
            }

            return _repositories[Key] as IGenericRepository<TEntity, TKey>;

        }
    }
}
