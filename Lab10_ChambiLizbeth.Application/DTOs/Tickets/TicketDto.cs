namespace Lab10_ChambiLizbeth.Application.DTOs.Tickets;

public record TicketDto(
    Guid TicketId,
    Guid UserId,
    string Title,
    string? Description,
    string Status,
    DateTime? CreatedAt,
    DateTime? ClosedAt);