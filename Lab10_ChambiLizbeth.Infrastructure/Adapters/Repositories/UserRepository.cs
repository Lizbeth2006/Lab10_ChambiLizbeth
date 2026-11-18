using Lab10_ChambiLizbeth.Domain.Entities;
using Lab10_ChambiLizbeth.Domain.Repositories;
using Lab10_ChambiLizbeth.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Lab10_ChambiLizbeth.Infrastructure.Adapters.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TicketeraDbContext _context;

    public UserRepository(TicketeraDbContext context)
    {
        _context = context;
    }

    public Task<User?> GetByIdAsync(Guid userId) =>
        _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);

    public Task<User?> GetByUsernameAsync(string username) =>
        _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Username == username);

    public Task<bool> ExistsAsync(string username, string? email) =>
        _context.Users.AnyAsync(u => u.Username == username
                                     || (email != null && u.Email == email));

    public async Task AddAsync(User user) =>
        await _context.Users.AddAsync(user);
}