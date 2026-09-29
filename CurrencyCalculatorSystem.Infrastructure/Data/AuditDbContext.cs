using CurrencyCalculatorSystem.Infrastructure.Services.LogActivity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Infrastructure.Data
{
    public class AuditDbContext :DbContext
    {
        public AuditDbContext(DbContextOptions<AuditDbContext> options) : base(options) { }

        public DbSet<Log> logs => Set<Log>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("AuditLogs");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Action)
                      .HasColumnType("nvarchar(max)") 
                      .IsRequired();

                entity.Property(e => e.Timestamp)
                      .HasDefaultValueSql("GETUTCDATE()");
            });
        }
    }
}
