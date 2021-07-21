using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace DynamicField.EntityFrameworkCore
{
    public static class DynamicFieldDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<DynamicFieldDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<DynamicFieldDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
