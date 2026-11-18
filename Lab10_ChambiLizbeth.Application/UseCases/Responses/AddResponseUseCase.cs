using Lab10_ChambiLizbeth.Application.Constants;
using Lab10_ChambiLizbeth.Application.DTOs.Responses;
using Lab10_ChambiLizbeth.Application.Mappings;
using Lab10_ChambiLizbeth.Domain.Entities;
using Lab10_ChambiLizbeth.Domain.Repositories;

namespace Lab10_ChambiLizbeth.Application.UseCases.Responses;

public class AddResponseUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public AddResponseUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseDto> ExecuteAsync(
        Guid ticketId, Guid userId, bool isStaff, AddResponseRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
            throw new ArgumentException("El mensaje es obligatorio.");

        var ticket = await _unitOfWork.Tickets.GetByIdAsync(ticketId);
        if (ticket is null || (!isStaff && ticket.UserId != userId))
            throw new KeyNotFoundException("Ticket no encontrado.");

        if (ticket.Status == TicketStatus.Cerrado)
            throw new InvalidOperationException("No se puede responder un ticket cerrado.");

        var response = new Response
        {
            ResponseId = Guid.NewGuid(),
            TicketId = ticketId,
            ResponderId = userId,
            Message = request.Message.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Responses.AddAsync(response);
        await _unitOfWork.SaveChangesAsync();

        return response.ToDto();
    }
}