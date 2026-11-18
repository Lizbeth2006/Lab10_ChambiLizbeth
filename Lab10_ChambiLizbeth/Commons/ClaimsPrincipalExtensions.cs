using System.Security.Claims;

namespace Lab10_ChambiLizbeth.Commons;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var id = user.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(id, out var userId)
            ? userId
            : throw new UnauthorizedAccessException("Token inválido.");
    }

    public static bool IsStaff(this ClaimsPrincipal user) =>
        user.IsInRole("Admin") || user.IsInRole("Soporte");
}