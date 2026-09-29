using CurrencyCalculatorSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.Contracts.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseDomainModel;
        ICatalogRepository<TCatalog> GetCatalogRepository<TCatalog>() where TCatalog : class, ICatalogEntity;
        Task<int> CompleteAsync();
    }
}
