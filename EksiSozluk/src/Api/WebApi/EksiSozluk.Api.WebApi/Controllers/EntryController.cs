using EksiSozluk.Common.ViewModels.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EksiSozluk.Api.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EntryController : BaseController
{
    private readonly IMediator _mediator;
    
    public EntryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Route("CreateEntry")]
    public async Task<IActionResult> CreateEntry([FromBody] CreateEntryCommand command)
    {
        if (!command.CreatedById.HasValue)
            command.CreatedById = UserId;
        
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPost]
    [Route("CreateEntryComment")]
    public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommand command)
    {
        if (!command.CreatedById.HasValue)
            command.CreatedById = UserId;
        
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}