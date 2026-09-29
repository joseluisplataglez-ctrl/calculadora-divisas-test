using CurrencyCalculatorSystem.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Infrastructure.Data.Configurations
{
    public class FavourtieCurrenciesConfiguration : BaseConfiguration<FavouriteCurrencies>
    {
        public override void Configure(EntityTypeBuilder<FavouriteCurrencies> builder)
        {
            base.Configure(builder);

            builder.ToTable("FavouriteCurrencies");

            builder.Property(x => x.Id)
                .HasColumnName("FavouriteCurrencyId");

            builder.Property(x => x.FavouriteCurrency)
                .HasColumnName("FavouriteCurrency")
                .HasMaxLength(150)
                .IsRequired();
        }
    }
}
