using Lab10_ChambiLizbeth.Application.DTOs.Responses;
using Lab10_ChambiLizbeth.Application.UseCases.Responses;
using Lab10_ChambiLizbeth.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_ChambiLizbeth.Controllers;

[ApiController]
[Authorize]
[Route("api/tickets/{ticketId:guid}/responses")]
public class ResponsesController : ControllerBase
{
    private readonly AddResponseUseCase _add;
    private readonly GetResponsesByTicketUseCase _getByTicket;

    public ResponsesController(AddResponseUseCase add, GetResponsesByTicketUseCase getByTicket)
    {
        _add = add;
        _getByTicket = getByTicket;
    }

    [HttpPost]
    public async Task<ActionResult<ResponseDto>> Add(Guid ticketId, AddResponseRequest request)
    {
        var response = await _add.ExecuteAsync(ticketId, User.GetUserId(), User.IsStaff(), request);
        return StatusCode(StatusCodes.Status201Created, response);
    }

    [HttpGet]
    public async Task<ActionResult<List<ResponseDto>>> GetByTicket(Guid ticketId) =>
        Ok(await _getByTicket.ExecuteAsync(ticketId, User.GetUserId(), User.IsStaff()));
}