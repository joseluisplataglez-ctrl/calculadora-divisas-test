using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.Contracts.Data
{
    public interface ISaveLog
    {
        void SaveLogAsync(string activiyLog);
    }
}
