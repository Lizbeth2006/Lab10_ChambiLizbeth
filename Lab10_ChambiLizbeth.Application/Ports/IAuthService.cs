namespace Lab10_ChambiLizbeth.Application.Ports;

public interface IAuthService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
    string GenerateToken(Guid userId, string username, IEnumerable<string> roles);
}