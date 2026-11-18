using Lab10_ChambiLizbeth.Domain.Entities;

namespace Lab10_ChambiLizbeth.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid userId);
    Task<User?> GetByUsernameAsync(string username);
    Task<bool> ExistsAsync(string username, string? email);
    Task AddAsync(User user);
}