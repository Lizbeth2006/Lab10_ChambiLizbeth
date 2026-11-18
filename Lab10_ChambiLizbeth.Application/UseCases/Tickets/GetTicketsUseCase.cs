using Lab10_ChambiLizbeth.Application.DTOs.Tickets;
using Lab10_ChambiLizbeth.Application.Mappings;
using Lab10_ChambiLizbeth.Domain.Repositories;

namespace Lab10_ChambiLizbeth.Application.UseCases.Tickets;

public class GetTicketsUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTicketsUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<TicketDto>> ExecuteAsync(Guid userId, bool isStaff)
    {
        var tickets = isStaff
            ? await _unitOfWork.Tickets.GetAllAsync()
            : await _unitOfWork.Tickets.GetByUserIdAsync(userId);

        return tickets.Select(t => t.ToDto()).ToList();
    }
}