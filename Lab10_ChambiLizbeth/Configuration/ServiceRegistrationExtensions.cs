using System.Text;
using Lab10_ChambiLizbeth.Application.UseCases.Users;
using Lab10_ChambiLizbeth.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Lab10_ChambiLizbeth.Application.UseCases.Tickets;
using Lab10_ChambiLizbeth.Application.UseCases.Responses;

namespace Lab10_ChambiLizbeth.Configuration;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        // Registro de servicios de Infraestructura
        services.AddInfrastructureServices(configuration);

        // Casos de uso (clases sin interfaz)
        services.AddScoped<RegisterUserUseCase>();
        services.AddScoped<LoginUseCase>();
        services.AddScoped<CreateTicketUseCase>();
        services.AddScoped<GetTicketsUseCase>();
        services.AddScoped<GetTicketByIdUseCase>();
        services.AddScoped<UpdateTicketStatusUseCase>();
        services.AddScoped<CloseTicketUseCase>();
        services.AddScoped<AddResponseUseCase>();
        services.AddScoped<GetResponsesByTicketUseCase>();

        // Configuración de autenticación JWT
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!))
                };
            });
        services.AddAuthorization();

        // Habilitar controladores
        services.AddControllers();

        // Habilitar Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Ticketera API",
                Version = "v1",
                Description = "API de tickets con arquitectura hexagonal, casos de uso y JWT."
            });

            // Botón "Authorize" para pegar el token
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Pega solo el token JWT (sin la palabra Bearer).",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }
}