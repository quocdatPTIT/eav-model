using System.Threading.Tasks;
using Abp.Application.Services;
using DynamicField.Tickets.Models.Request;
using DynamicField.Tickets.Models.Response;

namespace DynamicField.Tickets
{
    public interface IEavTicketAppService : IApplicationService
    {
        Task<bool> CreateAttribute(CreateAttributeReq res);
        Task<GetAttributeTicketRes> GetAttributesTicket();
    }
}