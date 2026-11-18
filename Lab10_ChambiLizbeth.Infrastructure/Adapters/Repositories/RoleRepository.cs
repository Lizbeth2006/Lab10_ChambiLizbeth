using Lab10_ChambiLizbeth.Domain.Entities;
using Lab10_ChambiLizbeth.Domain.Repositories;
using Lab10_ChambiLizbeth.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Lab10_ChambiLizbeth.Infrastructure.Adapters.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly TicketeraDbContext _context;

    public RoleRepository(TicketeraDbContext context)
    {
        _context = context;
    }

    public Task<Role?> GetByNameAsync(string roleName) =>
        _context.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
}