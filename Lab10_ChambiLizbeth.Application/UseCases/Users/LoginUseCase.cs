using Lab10_ChambiLizbeth.Application.DTOs.Users;
using Lab10_ChambiLizbeth.Application.Ports;
using Lab10_ChambiLizbeth.Domain.Repositories;

namespace Lab10_ChambiLizbeth.Application.UseCases.Users;

public class LoginUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;

    public LoginUseCase(IUnitOfWork unitOfWork, IAuthService authService)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
    }

    public async Task<LoginResponse> ExecuteAsync(LoginRequest request)
    {
        var user = await _unitOfWork.Users.GetByUsernameAsync(request.Username);

        // Mismo mensaje si falla el usuario o la contraseña, para no revelar cuál fue.
        if (user is null || !_authService.VerifyPassword(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Credenciales inválidas.");

        var roles = user.UserRoles.Select(ur => ur.Role.RoleName).ToList();
        var token = _authService.GenerateToken(user.UserId, user.Username, roles);

        return new LoginResponse(token);
    }
}