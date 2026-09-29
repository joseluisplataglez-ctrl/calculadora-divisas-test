using CurrencyCalculatorSystem.Application.Contracts.Data;
using CurrencyCalculatorSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Infrastructure.Services.LogActivity
{
    public class ActivityLog(
        AuditDbContext context
        ) : ISaveLog
    {
        public void SaveLogAsync(string activiyLog)
        {
            var log = new Log
            {
                Action = activiyLog
            };
            context.Add(log);
            context.SaveChanges();
        }
    }
}
