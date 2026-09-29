using CurrencyCalculatorSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.Contracts.Data
{
    public interface ICatalogRepository<T> where T : class, ICatalogEntity
    {
        
    }
}
