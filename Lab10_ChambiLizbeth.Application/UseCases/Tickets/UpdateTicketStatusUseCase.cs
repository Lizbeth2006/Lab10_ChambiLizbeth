using Lab10_ChambiLizbeth.Application.Constants;
using Lab10_ChambiLizbeth.Application.DTOs.Tickets;
using Lab10_ChambiLizbeth.Application.Mappings;
using Lab10_ChambiLizbeth.Domain.Repositories;

namespace Lab10_ChambiLizbeth.Application.UseCases.Tickets;

public class UpdateTicketStatusUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTicketStatusUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TicketDto> ExecuteAsync(Guid ticketId, UpdateTicketStatusRequest request)
    {
        if (!TicketStatus.All.Contains(request.Status))
            throw new ArgumentException(
                $"Estado inválido. Valores permitidos: {string.Join(", ", TicketStatus.All)}.");

        var ticket = await _unitOfWork.Tickets.GetByIdAsync(ticketId)
                     ?? throw new KeyNotFoundException("Ticket no encontrado.");

        ticket.Status = request.Status;
        ticket.ClosedAt = request.Status == TicketStatus.Cerrado ? DateTime.UtcNow : null;

        _unitOfWork.Tickets.Update(ticket);
        await _unitOfWork.SaveChangesAsync();

        return ticket.ToDto();
    }
}