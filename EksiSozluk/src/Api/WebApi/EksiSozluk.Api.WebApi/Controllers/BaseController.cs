using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace EksiSozluk.Api.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    public Guid? UserId => Guid.NewGuid(); // new(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
}