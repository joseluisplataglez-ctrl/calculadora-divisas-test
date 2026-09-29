using CurrencyCalculatorSystem.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Infrastructure.Data.Configurations
{
    public abstract class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseDomainModel
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

           
        }
    }
}
