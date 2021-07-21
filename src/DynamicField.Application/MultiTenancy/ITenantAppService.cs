using Abp.Application.Services;
using DynamicField.MultiTenancy.Dto;

namespace DynamicField.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

