using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            var stringBuilder = new System.Text.StringBuilder();
            var previousUpper = false;

            foreach (char c in input)
            {
                if (char.IsUpper(c))
                {
                    if (stringBuilder.Length > 0 && !previousUpper)
                        stringBuilder.Append('_');

                    stringBuilder.Append(char.ToLower(c));
                    previousUpper = true;
                }
                else
                {
                    stringBuilder.Append(c);
                    previousUpper = false;
                }
            }

            return stringBuilder.ToString();
        }

        public static void ToSnakeCaseTable<TEntity>(this EntityTypeBuilder<TEntity> builder, string schema)
            where TEntity : class
        {
            var entityName = typeof(TEntity).Name;
            builder.ToTable(entityName.ToSnakeCase(), schema);
        }

        public static void ToSnakeCaseColumns<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class
        {
            var properties = typeof(TEntity).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (var prop in properties)
            {
                // Solo mapea propiedades simples (no navegación)
                if (prop.PropertyType.IsPrimitive || prop.PropertyType == typeof(string) || prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?) || prop.PropertyType == typeof(Guid) || prop.PropertyType == typeof(Guid?) || prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?) || prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?))
                {
                    builder.Property(prop.Name).HasColumnName(prop.Name.ToSnakeCase());
                }
            }
        }
    }
}
