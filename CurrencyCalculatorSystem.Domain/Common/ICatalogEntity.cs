using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Domain.Common
{
    public interface ICatalogEntity
    {
        short Id { get; set; }
        string Name { get; set; }
        bool IsActive { get; set; }
    }
}
