using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using DynamicField.Configuration;
using DynamicField.Web;

namespace DynamicField.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class DynamicFieldDbContextFactory : IDesignTimeDbContextFactory<DynamicFieldDbContext>
    {
        public DynamicFieldDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DynamicFieldDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DynamicFieldDbContextConfigurer.Configure(builder, configuration.GetConnectionString(DynamicFieldConsts.ConnectionStringName));

            return new DynamicFieldDbContext(builder.Options);
        }
    }
}
