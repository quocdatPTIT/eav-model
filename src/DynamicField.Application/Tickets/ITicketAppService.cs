using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using DynamicField.Tickets.Models.Request;
using DynamicField.Tickets.Models.Response;

namespace DynamicField.Tickets
{
    public interface ITicketAppService : IApplicationService
    {
        Task<bool> CreateTicket(CreateTicketReq res);

        Task<GetTicketsRes> GetTickets(int skip, int take);
    }
}