using CurrencyCalculatorSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.Contracts.Data
{
    public interface IBaseRepository<T> where T : BaseDomainModel
    {
        #region Async Read Methods

        Task<IEnumerable<T>> GetAllAsync(
            List<string>? includes = null,
            bool disableTracking = true,
            EntityStatusFilter filter = EntityStatusFilter.OnlyActive
            );

        Task<T?> GetByIdAsync(
            int id,
            List<string>? includes = null, 
            EntityStatusFilter filter = EntityStatusFilter.OnlyActive 
        );
        Task<IEnumerable<T>> GetListAsync(
            Expression<Func<T, bool>> predicate, 
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, 
            List<string>? includes = null, 
            bool disableTracking = true,
            EntityStatusFilter filter = EntityStatusFilter.OnlyActive 
        );

        Task<bool> ExistAsync(
            Expression<Func<T, bool>> predicate,
            List<string>? includes = null,
            EntityStatusFilter filter = EntityStatusFilter.OnlyActive
        );


        #endregion

        #region Async Write Methods
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        T Update(T entity);
        IEnumerable<T> UpdateRange(IEnumerable<T> entities);
        Task DeleteAsync(Guid id);
        void Delete(T entity);
        Task DeleteRangeAsync(IEnumerable<string> ids);
        void DeleteRange(IEnumerable<T> entities);
        void DeleteAll();
        Task SoftDeleteAsync(Guid id);
        void SoftDelete(T entity);
        Task SoftDeleteRangeAsync(IEnumerable<string> ids);
        void SoftDeleteRange(IEnumerable<T> entities);
        void SoftDeleteAllAsync();
        #endregion

    }
}
