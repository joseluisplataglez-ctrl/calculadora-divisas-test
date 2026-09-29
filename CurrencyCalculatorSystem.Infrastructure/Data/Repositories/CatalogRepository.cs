using CurrencyCalculatorSystem.Application.Contracts.Data;
using CurrencyCalculatorSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Infrastructure.Data.Repositories
{
    public class CatalogRepository<T>(ContextDb dbContext) : ICatalogRepository<T> where T : class, ICatalogEntity
    {
       
       
    }
}
