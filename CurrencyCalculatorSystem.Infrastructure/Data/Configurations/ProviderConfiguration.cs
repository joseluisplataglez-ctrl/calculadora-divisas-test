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
    public class ProviderConfiguration : BaseConfiguration<Provider>
    {
        public override void Configure(EntityTypeBuilder<Provider> builder)
        {
            base.Configure(builder);

            builder.ToTable("Providers");

            builder.Property(x => x.Id)
                .HasColumnName("ProviderId");

            builder.Property(x => x.ProviderKey)
                .HasColumnName("ProviderKey")
                .HasMaxLength(50) 
                .IsRequired();

            builder.Property(x => x.ProviderName)
                .HasColumnName("ProviderName")
                .HasMaxLength(100) 
                .IsRequired();

            builder.Property(x => x.ProviderCountryCode)
                .HasColumnName("ProviderCountryCode")
                .HasMaxLength(2) 
                .IsFixedLength()
                .IsRequired();

            builder.Property(x => x.ProviderRateType)
                .HasColumnName("ProviderRateType")
                .HasMaxLength(50) 
                .IsRequired();

            builder.Property(x => x.ProviderPivotCurrency)
                .HasColumnName("ProviderPivotCurrency")
                .HasMaxLength(3) 
                .IsFixedLength()
                .IsRequired();

            builder.HasIndex(x => x.ProviderKey)
                .IsUnique()
                .HasDatabaseName("UX_Providers_ProviderKey");
        }
    }
}
