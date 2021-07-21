using System.Threading.Tasks;
using Abp.Application.Services;
using DynamicField.Sessions.Dto;

namespace DynamicField.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
