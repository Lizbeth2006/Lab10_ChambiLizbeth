using Lab10_ChambiLizbeth.Application.DTOs.Responses;
using Lab10_ChambiLizbeth.Application.DTOs.Tickets;
using Lab10_ChambiLizbeth.Domain.Entities;

namespace Lab10_ChambiLizbeth.Application.Mappings;

public static class TicketMappings
{
    public static TicketDto ToDto(this Ticket t) =>
        new(t.TicketId, t.UserId, t.Title, t.Description, t.Status, t.CreatedAt, t.ClosedAt);

    public static ResponseDto ToDto(this Response r) =>
        new(r.ResponseId, r.TicketId, r.ResponderId, r.Message, r.CreatedAt);
}