using Lab10_ChambiLizbeth.Application.DTOs.Users;
using Lab10_ChambiLizbeth.Application.Ports;
using Lab10_ChambiLizbeth.Domain.Entities;
using Lab10_ChambiLizbeth.Domain.Repositories;

namespace Lab10_ChambiLizbeth.Application.UseCases.Users;

public class RegisterUserUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;

    public RegisterUserUseCase(IUnitOfWork unitOfWork, IAuthService authService)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
    }

    public async Task<RegisterUserResponse> ExecuteAsync(RegisterUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            throw new ArgumentException("El usuario y la contraseña son obligatorios.");

        if (request.Password.Length < 6)
            throw new ArgumentException("La contraseña debe tener al menos 6 caracteres.");

        if (await _unitOfWork.Users.ExistsAsync(request.Username, request.Email))
            throw new InvalidOperationException("El usuario o el email ya está registrado.");

        var role = await _unitOfWork.Roles.GetByNameAsync("Cliente")
                   ?? throw new InvalidOperationException("El rol 'Cliente' no existe en la base de datos.");

        var user = new User
        {
            UserId = Guid.NewGuid(),
            Username = request.Username,
            Email = request.Email,
            PasswordHash = _authService.HashPassword(request.Password),
            CreatedAt = DateTime.UtcNow
        };

        user.UserRoles.Add(new UserRole
        {
            UserId = user.UserId,
            RoleId = role.RoleId,
            AssignedAt = DateTime.UtcNow
        });

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();   // guarda usuario y rol en una sola operación

        return new RegisterUserResponse(user.UserId, user.Username);
    }
}