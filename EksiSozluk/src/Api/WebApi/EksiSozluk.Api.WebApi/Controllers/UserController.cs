using EksiSozluk.Api.Application.Features.Commands.User.ConfirmEmail;
using EksiSozluk.Api.Application.Features.Queries.GetUserDetail;
using EksiSozluk.Common.Events.User;
using EksiSozluk.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EksiSozluk.Api.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : BaseController
{
    private readonly IMediator _mediator;
    
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var user = await _mediator.Send(new GetUserDetailQuery(id));
        return Ok(user);
    }
    
    [HttpGet]
    [Route("UserName/{userName}")]
    public async Task<IActionResult> GetByUserName(string userName)
    {
        var user = await _mediator.Send(new GetUserDetailQuery(Guid.Empty, userName));
        return Ok(user);
    }
    
   [HttpPost]
   [Route("Login")]
   public async Task<IActionResult> Login([FromBody]LoginUserCommand command)
   {
       var res = await _mediator.Send(command);
         return Ok(res);
   }
   
   [HttpPost]
   public async Task<IActionResult> Create([FromBody]CreateUserCommand command)
   {
       var guid = await _mediator.Send(command); 
       return Ok(guid);
   }
   
   [HttpPost]
   [Route("Update")]
   [Authorize]
   public async Task<IActionResult> UpdateUser([FromBody]UpdateUserCommand command)
   {
       var guid = await _mediator.Send(command); 
       return Ok(guid);
   }
   
   [HttpPost]
   [Route("Confirm")]
   public async Task<IActionResult> ConfirmEmail(Guid confirmationId)
   {
       var res = await _mediator.Send(new ConfirmEmailCommand { ConfirmationId = confirmationId });
       return Ok(res);
   }
   
   [HttpPost]
   [Route("ChangePassword")]
   [Authorize]
   public async Task<IActionResult> ChangePassword([FromBody]ChangeUserPasswordCommand command)
   {
       if (!command.UserId.HasValue)
           command.UserId = UserId;
       
       var res = await _mediator.Send(command);
       return Ok(res);
   }
}