using Lab10_ChambiLizbeth.Application.Constants;
using Lab10_ChambiLizbeth.Application.DTOs.Tickets;
using Lab10_ChambiLizbeth.Application.Mappings;
using Lab10_ChambiLizbeth.Domain.Entities;
using Lab10_ChambiLizbeth.Domain.Repositories;

namespace Lab10_ChambiLizbeth.Application.UseCases.Tickets;

public class CreateTicketUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTicketUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TicketDto> ExecuteAsync(Guid userId, CreateTicketRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            throw new ArgumentException("El título es obligatorio.");

        if (request.Title.Length > 255)
            throw new ArgumentException("El título no puede superar los 255 caracteres.");

        var ticket = new Ticket
        {
            TicketId = Guid.NewGuid(),
            UserId = userId,
            Title = request.Title.Trim(),
            Description = request.Description,
            Status = TicketStatus.Abierto,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Tickets.AddAsync(ticket);
        await _unitOfWork.SaveChangesAsync();

        return ticket.ToDto();
    }
}