using AutoMapper;
using EksiSozluk.Api.Application.Features.Queries.GetEntiries;
using EksiSozluk.Api.Application.Features.Queries.GetEntryComments;
using EksiSozluk.Api.Application.Features.Queries.GetEntryDetail;
using EksiSozluk.Api.Application.Features.Queries.GetMainPageEntries;
using EksiSozluk.Api.Application.Features.Queries.GetUserEntries;
using EksiSozluk.Api.Application.Interfaces.Repositories;
using EksiSozluk.Common.Models.Queries;
using EksiSozluk.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EksiSozluk.Api.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EntryController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IEntryRepository _entryRepository;
    private readonly IMapper _mapper;
    
    public EntryController(IMediator mediator, IEntryRepository entryRepository, IMapper mapper)
    {
        _mediator = mediator;
        _entryRepository = entryRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetEntries([FromQuery]GetEntriesQuery query)
    {
        var entries = await _mediator.Send(query);
        return Ok(entries);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetEntryDetailQuery(id, UserId));
        return Ok(result);
    }
    
    [HttpGet]
    [Route("Comments/{id}")]
    public async Task<IActionResult> GetEntryComments(Guid id, int page, int pageSize)
    {
        var result = await _mediator.Send(new GetEntryCommentsQuery(id, UserId, page, pageSize));
        return Ok(result);
    }
    
    [HttpGet]
    [Route("UserEntries")]
    [Authorize]
    public async Task<IActionResult> GetUserEntries(string userName, Guid userId, int page, int pageSize)
    {
        if (userId == Guid.Empty && string.IsNullOrEmpty(userName))
            userId = UserId.Value;
        
        var result = await _mediator.Send(new GetUserEntriesQuery(userId, userName, page, pageSize));
        return Ok(result);
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
    [Authorize]
    public async Task<IActionResult> CreateEntry([FromBody] CreateEntryCommand command)
    {
        if (!command.CreatedById.HasValue)
            command.CreatedById = UserId;
        
        // var result = await _mediator.Send(command);
        // return Ok(result);
        
        var dbEntry = _mapper.Map<Domain.Models.Entry>(command);
        
        await _entryRepository.AddAsync(dbEntry);

        return Ok(dbEntry.Id);
    }
    
    [HttpPost]
    [Route("CreateEntryComment")]
    [Authorize]
    public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommand command)
    {
        if (!command.CreatedById.HasValue)
            command.CreatedById = UserId;
        
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpGet]
    [Route("Search")]
    public async Task<IActionResult> Search([FromQuery]SearchEntryQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}