using Lab10_ChambiLizbeth.Domain.Entities;

namespace Lab10_ChambiLizbeth.Domain.Repositories;

public interface ITicketRepository
{
    Task AddAsync(Ticket ticket);
    Task<Ticket?> GetByIdAsync(Guid ticketId);
    Task<IEnumerable<Ticket>> GetAllAsync();
    Task<IEnumerable<Ticket>> GetByUserIdAsync(Guid userId);
    void Update(Ticket ticket);
}