using Lab10_ChambiLizbeth.Application.DTOs.Tickets;
using Lab10_ChambiLizbeth.Application.UseCases.Tickets;
using Lab10_ChambiLizbeth.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_ChambiLizbeth.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly CreateTicketUseCase _create;
    private readonly GetTicketsUseCase _getAll;
    private readonly GetTicketByIdUseCase _getById;
    private readonly UpdateTicketStatusUseCase _updateStatus;
    private readonly CloseTicketUseCase _close;

    public TicketsController(
        CreateTicketUseCase create,
        GetTicketsUseCase getAll,
        GetTicketByIdUseCase getById,
        UpdateTicketStatusUseCase updateStatus,
        CloseTicketUseCase close)
    {
        _create = create;
        _getAll = getAll;
        _getById = getById;
        _updateStatus = updateStatus;
        _close = close;
    }

    [HttpPost]
    public async Task<ActionResult<TicketDto>> Create(CreateTicketRequest request)
    {
        var ticket = await _create.ExecuteAsync(User.GetUserId(), request);
        return CreatedAtAction(nameof(GetById), new { id = ticket.TicketId }, ticket);
    }

    [HttpGet]
    public async Task<ActionResult<List<TicketDto>>> GetAll() =>
        Ok(await _getAll.ExecuteAsync(User.GetUserId(), User.IsStaff()));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TicketDto>> GetById(Guid id) =>
        Ok(await _getById.ExecuteAsync(id, User.GetUserId(), User.IsStaff()));

    [Authorize(Roles = "Admin,Soporte")]
    [HttpPut("{id:guid}/status")]
    public async Task<ActionResult<TicketDto>> UpdateStatus(Guid id, UpdateTicketStatusRequest request) =>
        Ok(await _updateStatus.ExecuteAsync(id, request));

    [Authorize(Roles = "Admin,Soporte")]
    [HttpPut("{id:guid}/close")]
    public async Task<ActionResult<TicketDto>> Close(Guid id) =>
        Ok(await _close.ExecuteAsync(id));
}