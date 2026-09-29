using CurrencyCalculatorSystem.Application.Contracts.ContextApplication;
using CurrencyCalculatorSystem.Domain.Aggregates;
using CurrencyCalculatorSystem.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CurrencyCalculatorSystem.Infrastructure.Data
{
    public class ContextDb(DbContextOptions<ContextDb> options, ICurrentUserProvider currentUserProvider) : DbContext(options)
    {
        private readonly ICurrentUserProvider _currentUserProvider = currentUserProvider;
        public DbSet<Rate> Rates => Set<Rate>();
        public DbSet<FavouriteCurrencies> FavouriteCurrencies => Set<FavouriteCurrencies>();
        public DbSet<Provider> Providers => Set<Provider>();
        public DbSet<Currency> Currencies => Set<Currency>();

        // Sobreescribir el metodo SaveChangesAsync para que se actualicen las propiedades de auditoria
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var currentUserId = _currentUserProvider.Email;
            foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.MarkAsCreated(currentUserId);
                        break;
                    case EntityState.Modified:
                        entry.Entity.MarkAsUpdated(currentUserId);
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextDb).Assembly);

        }
    }
}
