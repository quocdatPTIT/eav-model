using System.Threading.Tasks;
using Abp.Application.Services;
using DynamicField.Tickets.Models.Request;

namespace DynamicField.Tickets
{
    public interface IEavTicketAppService : IApplicationService
    {
        Task<bool> CreateAttribute(CreateAttributeReq res);
    }
}