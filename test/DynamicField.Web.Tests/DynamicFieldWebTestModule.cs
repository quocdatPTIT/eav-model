using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using DynamicField.EntityFrameworkCore;
using DynamicField.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace DynamicField.Web.Tests
{
    [DependsOn(
        typeof(DynamicFieldWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class DynamicFieldWebTestModule : AbpModule
    {
        public DynamicFieldWebTestModule(DynamicFieldEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DynamicFieldWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(DynamicFieldWebMvcModule).Assembly);
        }
    }
}