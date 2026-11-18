using Lab10_ChambiLizbeth.Application.Constants;
using Lab10_ChambiLizbeth.Application.DTOs.Tickets;
using Lab10_ChambiLizbeth.Application.Mappings;
using Lab10_ChambiLizbeth.Domain.Repositories;

namespace Lab10_ChambiLizbeth.Application.UseCases.Tickets;

public class CloseTicketUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CloseTicketUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TicketDto> ExecuteAsync(Guid ticketId)
    {
        var ticket = await _unitOfWork.Tickets.GetByIdAsync(ticketId)
                     ?? throw new KeyNotFoundException("Ticket no encontrado.");

        if (ticket.Status == TicketStatus.Cerrado)
            throw new InvalidOperationException("El ticket ya está cerrado.");

        ticket.Status = TicketStatus.Cerrado;
        ticket.ClosedAt = DateTime.UtcNow;

        _unitOfWork.Tickets.Update(ticket);
        await _unitOfWork.SaveChangesAsync();

        return ticket.ToDto();
    }
}