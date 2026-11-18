using Lab10_ChambiLizbeth.Domain.Entities;

namespace Lab10_ChambiLizbeth.Domain.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetByNameAsync(string roleName);
}