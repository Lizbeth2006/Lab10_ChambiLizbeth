using Lab10_ChambiLizbeth.Domain.Entities;

namespace Lab10_ChambiLizbeth.Domain.Repositories;

public interface IResponseRepository
{
    Task AddAsync(Response response);
    Task<IEnumerable<Response>> GetByTicketIdAsync(Guid ticketId);
}