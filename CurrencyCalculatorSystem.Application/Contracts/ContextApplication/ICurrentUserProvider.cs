using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.Contracts.ContextApplication
{
    public interface ICurrentUserProvider
    {
        Guid UserId { get; }
        string Name { get; }
        string Email { get; }
        string Department { get; }
        string OfficeLocation { get; }
        string? GetToken();
    }
}
