using EksiSozluk.Api.Application.Features.Queries.GetEntiries;
using EksiSozluk.Api.Application.Features.Queries.GetMainPageEntries;
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

    [HttpGet]
    public async Task<IActionResult> GetEntries([FromQuery]GetEntriesQuery query)
    {
        var entries = await _mediator.Send(query);
        return Ok(entries);
    }
    
    [HttpGet]
    [Route("MainPageEntries")]
    public async Task<IActionResult> GetMainPageEntries(int page, int pageSize)
    {
        var entries = await _mediator.Send(new GetMainPageEntriesQuery(UserId, page, pageSize));
        return Ok(entries);
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