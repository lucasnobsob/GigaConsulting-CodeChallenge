using Microsoft.EntityFrameworkCore;

namespace GigaConsulting.Infra.Data.DBSpecifications
{
    public static class MySqlDBSpecification
    {
        public static void ConfigureProperties(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var schema = entity.GetSchema();
                if (!string.IsNullOrEmpty(schema))
                {
                    entity.SetSchema(null);
                }

                foreach (var property in entity.GetProperties())
                {
                    var clrType = property.ClrType;

                    if (clrType == typeof(Guid))
                    {
                        property.SetColumnType("char(36)");
                    }

                    if (clrType == typeof(DateTime) || clrType == typeof(DateTime?))
                    {
                        property.SetColumnType("datetime(6)");
                    }

                    var defaultValueSql = property.GetDefaultValueSql();
                    if (!string.IsNullOrEmpty(defaultValueSql))
                    {
                        if (defaultValueSql.Contains("GETDATE", StringComparison.OrdinalIgnoreCase))
                        {
                            property.SetDefaultValueSql(null);
                        }

                    }

                    if (property.GetColumnType()?.StartsWith("nvarchar") == true)
                    {
                        property.SetColumnType("varchar(255)");
                    }
                }
            }
        }
    }
}
