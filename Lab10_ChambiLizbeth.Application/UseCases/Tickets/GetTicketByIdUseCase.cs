using Lab10_ChambiLizbeth.Application.DTOs.Tickets;
using Lab10_ChambiLizbeth.Application.Mappings;
using Lab10_ChambiLizbeth.Domain.Repositories;

namespace Lab10_ChambiLizbeth.Application.UseCases.Tickets;

public class GetTicketByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTicketByIdUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TicketDto> ExecuteAsync(Guid ticketId, Guid userId, bool isStaff)
    {
        var ticket = await _unitOfWork.Tickets.GetByIdAsync(ticketId);

        // Si no existe o no es suyo (y no es staff), mismo mensaje: no revelamos que existe.
        if (ticket is null || (!isStaff && ticket.UserId != userId))
            throw new KeyNotFoundException("Ticket no encontrado.");

        return ticket.ToDto();
    }
}