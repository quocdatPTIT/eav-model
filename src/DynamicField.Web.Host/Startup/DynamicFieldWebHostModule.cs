using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using DynamicField.Configuration;

namespace DynamicField.Web.Host.Startup
{
    [DependsOn(
       typeof(DynamicFieldWebCoreModule))]
    public class DynamicFieldWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public DynamicFieldWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DynamicFieldWebHostModule).GetAssembly());
        }
    }
}
