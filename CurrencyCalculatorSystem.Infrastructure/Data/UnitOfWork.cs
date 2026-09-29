using CurrencyCalculatorSystem.Application.Contracts.Data;
using CurrencyCalculatorSystem.Domain.Common;
using CurrencyCalculatorSystem.Infrastructure.Data.Repositories;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using FluentValidation;

namespace CurrencyCalculatorSystem.Infrastructure.Data
{
    public class UnitOfWork(ContextDb dbContext) : IUnitOfWork
    {
        private Hashtable? _repositories;
        private readonly ContextDb _dbContext = dbContext;

        public async Task<int> CompleteAsync()
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var result = await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                await transaction.RollbackAsync();
                throw new InvalidOperationException("El registro no coincide con la vetsión de la base de datos (Error de concurrencia).", ex);

            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                await transaction.RollbackAsync();
                var failures = new List<ValidationFailure>();
                switch (sqlEx.Number)
                {
                    case 547: // Error de llave foránea (Foreign Key Violation / Delete conflict)
                        if (sqlEx.Message.Contains("DELETE", StringComparison.OrdinalIgnoreCase))
                        {
                            failures.Add(new ValidationFailure("Id", "No se puede eliminar el registro porque tiene dependencias activas."));
                        }
                        else
                        {
                            failures.Add(new ValidationFailure("DocumentNameId", "Error de consistencia: El Id proporcionado no existe en el catálogo."));
                        }
                        break;

                    case 2601: // Duplicado en índice único
                    case 2627: // Violación de llave primaria o restricción UNIQUE
                        failures.Add(new ValidationFailure("Name", "Operación duplicada: Ya existe un registro con estos datos únicos."));
                        break;

                    case 515: // Error al insertar un valor NULL en una columna obligatoria
                        failures.Add(new ValidationFailure("Campo", "Este campo es obligatorio y no puede quedar vacío."));
                        break;

                    default:
                        failures.Add(new ValidationFailure("Database", "Ocurrió un error inesperado al persistir los cambios relacionales."));
                        break;
                }

                throw new ValidationException(failures);

            }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseDomainModel
        {
            _repositories ??= [];
            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(BaseRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dbContext);
                _repositories.Add(type, repositoryInstance);
            }

            return (IBaseRepository<TEntity>)_repositories[type]!;
        }

        ICatalogRepository<TCatalog> IUnitOfWork.GetCatalogRepository<TCatalog>()
        {
            _repositories ??= [];

            var key = $"{typeof(TCatalog).Name}";

            if (!_repositories.ContainsKey(key))
            {
                var repositoryType = typeof(CatalogRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TCatalog)), _dbContext);
                _repositories.Add(key, repositoryInstance);
            }

            return (ICatalogRepository<TCatalog>)_repositories[key]!;
        }
    }
}
