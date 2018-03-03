using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreTest.Context;
using CoreTest.Models.Entities;
using CoreTest.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoreTest.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : Entity
    {
        protected CDEContext Context;

        public BaseRepository(CDEContext context)
        {
            Context = context;
        }

        public Task<IEnumerable<TEntity>> GetAll()
        {
            return Task.FromResult(Context.EntitySet<TEntity>().AsEnumerable());
        }

        public Task<TEntity> GetById(int id)
        {
            return Task.FromResult(Context.EntitySet<TEntity>().FirstOrDefault(x => x.Id == id));
        }

        public Task<IQueryable<TEntity>> GetQuery(bool asNoTracking)
        {
            if (asNoTracking)
            {
                return Task.FromResult(Context.EntitySet<TEntity>().AsNoTracking());
            }

            return Task.FromResult(Context.EntitySet<TEntity>().AsQueryable());
        }

        public Task<IQueryable<TEntity>> GetQueryAsNoTracking()
        {
            return GetQuery(true);
        }

        /// <summary>
        /// Returns items based on the linq where filter provided
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter)
        {
            return Context.EntitySet<TEntity>()
                            .Where(filter);
        }
    }
}
