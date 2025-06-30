using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProjectName.Api.Controllers;

[ApiVersion("1")]
[Route("v{version:apiVersion}/[controller]")]
[ApiController]
//[Authorize]
public class UsersController : BaseApiController<UsersController>
{
    public UsersController(IMediator mediator, ILogger<UsersController> logger) : base(mediator, logger)
    {
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok("Users");
    }
}
