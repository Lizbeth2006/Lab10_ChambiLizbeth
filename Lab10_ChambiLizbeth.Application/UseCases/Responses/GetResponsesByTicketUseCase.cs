using Lab10_ChambiLizbeth.Application.DTOs.Responses;
using Lab10_ChambiLizbeth.Application.Mappings;
using Lab10_ChambiLizbeth.Domain.Repositories;

namespace Lab10_ChambiLizbeth.Application.UseCases.Responses;

public class GetResponsesByTicketUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetResponsesByTicketUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ResponseDto>> ExecuteAsync(Guid ticketId, Guid userId, bool isStaff)
    {
        var ticket = await _unitOfWork.Tickets.GetByIdAsync(ticketId);
        if (ticket is null || (!isStaff && ticket.UserId != userId))
            throw new KeyNotFoundException("Ticket no encontrado.");

        var responses = await _unitOfWork.Responses.GetByTicketIdAsync(ticketId);
        return responses.Select(r => r.ToDto()).ToList();
    }
}