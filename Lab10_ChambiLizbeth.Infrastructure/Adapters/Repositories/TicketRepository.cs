using Lab10_ChambiLizbeth.Domain.Entities;
using Lab10_ChambiLizbeth.Domain.Repositories;
using Lab10_ChambiLizbeth.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Lab10_ChambiLizbeth.Infrastructure.Adapters.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly TicketeraDbContext _context;

    public TicketRepository(TicketeraDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Ticket ticket) =>
        await _context.Tickets.AddAsync(ticket);

    public Task<Ticket?> GetByIdAsync(Guid ticketId) =>
        _context.Tickets.FirstOrDefaultAsync(t => t.TicketId == ticketId);

    public async Task<IEnumerable<Ticket>> GetAllAsync() =>
        await _context.Tickets
            .AsNoTracking()
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();

    public async Task<IEnumerable<Ticket>> GetByUserIdAsync(Guid userId) =>
        await _context.Tickets
            .AsNoTracking()
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();

    public void Update(Ticket ticket) =>
        _context.Tickets.Update(ticket);
}