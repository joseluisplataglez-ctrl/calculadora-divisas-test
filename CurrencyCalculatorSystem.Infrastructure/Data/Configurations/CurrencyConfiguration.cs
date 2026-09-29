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
    public class CurrencyConfiguration : BaseConfiguration<Currency>
    {
        public override void Configure(EntityTypeBuilder<Currency> builder)
        {            
            base.Configure(builder);

            builder.ToTable("Currencies");

            builder.Property(x => x.Id)
                .HasColumnName("CurrencyId");

            builder.Property(x => x.CurrencyIsoCode)
                .HasColumnName("CurrencyIsoCode")
                .HasMaxLength(3)
                .IsFixedLength() 
                .IsRequired();

            builder.Property(x => x.CurrencyIsoNumber)
                .HasColumnName("CurrencyIsoNumber")
                .HasMaxLength(3)
                .IsFixedLength()
                .IsRequired();

            builder.Property(x => x.CurrencyName)
                .HasColumnName("CurrencyName")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.CurrencySymbol)
                .HasColumnName("CurrencySymbol")
                .HasMaxLength(10) 
                .IsRequired();

            builder.Property(x => x.CurrencyStartDate)
                .HasColumnName("CurrencyStartDate")
                .HasColumnType("date")
                .IsRequired();

            builder.HasIndex(x => x.CurrencyIsoCode)
                .IsUnique()
                .HasDatabaseName("UX_Currencies_IsoCode");
        }
    }
}