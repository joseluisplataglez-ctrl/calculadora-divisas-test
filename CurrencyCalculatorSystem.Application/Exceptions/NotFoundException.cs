using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.Exceptions
{
    public class NotFoundException(string entityTypes, string id) : Exception($"Entity {entityTypes} with ID {id} was not found.")
    {
        public string EntityType { get; } = entityTypes;
        public string Id { get; } = id;
    }
}
