using Lab10_ChambiLizbeth.Domain.Repositories;
using Lab10_ChambiLizbeth.Infrastructure.Context;

namespace Lab10_ChambiLizbeth.Infrastructure.Adapters.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly TicketeraDbContext _context;

    public UnitOfWork(TicketeraDbContext context)
    {
        _context = context;
        Users = new UserRepository(context);
        Roles = new RoleRepository(context);
        Tickets = new TicketRepository(context);
        Responses = new ResponseRepository(context);
    }

    public IUserRepository Users { get; }
    public IRoleRepository Roles { get; }
    public ITicketRepository Tickets { get; }
    public IResponseRepository Responses { get; }

    public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
}