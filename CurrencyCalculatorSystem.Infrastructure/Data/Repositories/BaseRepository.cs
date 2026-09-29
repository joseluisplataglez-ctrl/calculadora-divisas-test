using CurrencyCalculatorSystem.Application.Contracts.Data;
using CurrencyCalculatorSystem.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Infrastructure.Data.Repositories
{
    public class BaseRepository<T>(ContextDb dbContext) : IBaseRepository<T> where T : BaseDomainModel
    {
        private readonly ContextDb _dbContext = dbContext;

        #region Read Methods
        public async Task<IEnumerable<T>> GetAllAsync(
            List<string>? includes = null,
            bool disableTracking = true,
            EntityStatusFilter filter = EntityStatusFilter.OnlyActive
        )
        {
            IQueryable<T> query = BaseRepository<T>.ApplyEntityStatusFilter(_dbContext.Set<T>(), filter);

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(
            int id,
            List<string>? includes = null,
            EntityStatusFilter filter = EntityStatusFilter.OnlyActive
        )
        {
            IQueryable<T> query = BaseRepository<T>.ApplyEntityStatusFilter(_dbContext.Set<T>(), filter);

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<T>> GetListAsync(
                Expression<Func<T, bool>> predicate,
                Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                List<string>? includes = null,
                bool disableTracking = true,
                EntityStatusFilter filter = EntityStatusFilter.OnlyActive
            )
        {
            IQueryable<T> query = BaseRepository<T>.ApplyEntityStatusFilter(_dbContext.Set<T>(), filter);

            query = query.Where(predicate);

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }

        public async Task<bool> ExistAsync(
                Expression<Func<T, bool>> predicate,
                List<string>? includes = null,
                EntityStatusFilter filter = EntityStatusFilter.OnlyActive
            )
        {
            IQueryable<T> query = BaseRepository<T>.ApplyEntityStatusFilter(_dbContext.Set<T>(), filter);

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return await query.AnyAsync(predicate);
        }

        #endregion

        #region Write Methods
        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public T Update(T entity)
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            entities.ToList().ForEach(e =>
            {
                _dbContext.Set<T>().Attach(e);
                _dbContext.Entry(e).State = EntityState.Modified;
            });

            return entities;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            _dbContext.Set<T>().Remove(entity!);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public async Task DeleteRangeAsync(IEnumerable<int> ids)
        {
            var entities = await _dbContext.Set<T>().Where(e => ids.Contains(e.Id)).ToListAsync();
            _dbContext.Set<T>().RemoveRange(entities);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }

        public void DeleteAll()
        {
            _dbContext.Set<T>().RemoveRange(_dbContext.Set<T>());
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            entity!.IsActive = false;
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void SoftDelete(T entity)
        {
            entity.IsActive = false;
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task SoftDeleteRangeAsync(IEnumerable<int> ids)
        {
            var entities = await _dbContext.Set<T>().Where(e => ids.Contains(e.Id)).ToListAsync();
            entities.ForEach(e =>
            {
                e.IsActive = false;
                _dbContext.Set<T>().Attach(e);
                _dbContext.Entry(e).State = EntityState.Modified;
            });
        }

        public void SoftDeleteRange(IEnumerable<T> entities)
        {
            entities.ToList().ForEach(e =>
            {
                e.IsActive = false;
                _dbContext.Set<T>().Attach(e);
                _dbContext.Entry(e).State = EntityState.Modified;
            });
        }

        public void SoftDeleteAllAsync()
        {
            _dbContext.Set<T>().ToList().ForEach(e =>
            {
                e.IsActive = false;
                _dbContext.Set<T>().Attach(e);
                _dbContext.Entry(e).State = EntityState.Modified;
            });
        }

        #endregion

        private static IQueryable<T> ApplyEntityStatusFilter(IQueryable<T> query, EntityStatusFilter filter)
        {
            return filter switch
            {
                EntityStatusFilter.OnlyActive => query.Where(x => x.IsActive),
                EntityStatusFilter.OnlyInactive => query.Where(x => !x.IsActive),
                _ => query,
            };
        }

        public Task DeleteRangeAsync(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public Task SoftDeleteRangeAsync(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }
    }

}
