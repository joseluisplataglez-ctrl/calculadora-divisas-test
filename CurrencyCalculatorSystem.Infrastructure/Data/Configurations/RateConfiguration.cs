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
    public class RateConfiguration : BaseConfiguration<Rate>
    {
        public override void Configure(EntityTypeBuilder<Rate> builder)
        {
            base.Configure(builder);

            builder.ToTable("Rates");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("RateId");

            builder.Property(x => x.CurrencyDate)
                .HasColumnName("CurrencyDate")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(x => x.CurrencyBaseId)
             .HasColumnName("CurrencyBaseId")
             .IsRequired();

            builder.Property(x => x.QuoteCurrencyId)
                .HasColumnName("QuoteCurrencyId")
                .IsRequired();

            builder.Property(x => x.RateValue)
                .HasColumnName("RateValue")
                .HasColumnType("decimal(18,4)")
                .IsRequired();

            builder.HasOne(x => x.Provider)
                .WithMany(p => p.Rates)
                .HasForeignKey(x => x.ProviderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CurrencyBase)
                .WithMany(c => c.BaseRates)
                .HasForeignKey(x => x.CurrencyBaseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.QuoteCurrency)
                .WithMany(c => c.QuoteRates)
                .HasForeignKey(x => x.QuoteCurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.CurrencyDate, x.CurrencyBaseId, x.QuoteCurrencyId, x.ProviderId })
        .IsUnique()
        .HasDatabaseName("UX_Rates_Date_Base_Quote_Provider");
        }
    }
}
