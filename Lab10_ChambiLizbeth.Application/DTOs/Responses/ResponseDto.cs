namespace Lab10_ChambiLizbeth.Application.DTOs.Responses;

public record ResponseDto(
    Guid ResponseId,
    Guid TicketId,
    Guid ResponderId,
    string Message,
    DateTime? CreatedAt);