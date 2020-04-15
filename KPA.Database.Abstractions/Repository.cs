using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KPA.Database.Abstractions
{
    public abstract class Repository<TContext, TEntity> : IRepository<TEntity> where TContext : DbContext where TEntity : class
    {

        private readonly TContext context;

        public Repository(TContext context)
        {
            this.context = context;
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            var added = context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return added.Entity;
        }

        public async Task Delete(TEntity entity)
        {
            var deleted = context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().FirstOrDefaultAsync(predicate, CancellationToken.None);
        }

        public Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public Task<List<TEntity>> GetAll()
        {
            return context.Set<TEntity>().ToListAsync();
        }

        public Task<TEntity> GetById(int id)
        {
            return context.Set<TEntity>().FindAsync(id).AsTask();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            var updated = context.Set<TEntity>().Attach(entity);
            await context.SaveChangesAsync();
            return updated.Entity;
        }
    }
}
