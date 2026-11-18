using Lab10_ChambiLizbeth.Domain.Entities;
using Lab10_ChambiLizbeth.Domain.Repositories;
using Lab10_ChambiLizbeth.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Lab10_ChambiLizbeth.Infrastructure.Adapters.Repositories;

public class ResponseRepository : IResponseRepository
{
    private readonly TicketeraDbContext _context;

    public ResponseRepository(TicketeraDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Response response) =>
        await _context.Responses.AddAsync(response);

    public async Task<IEnumerable<Response>> GetByTicketIdAsync(Guid ticketId) =>
        await _context.Responses
            .AsNoTracking()
            .Where(r => r.TicketId == ticketId)
            .OrderBy(r => r.CreatedAt)
            .ToListAsync();
}