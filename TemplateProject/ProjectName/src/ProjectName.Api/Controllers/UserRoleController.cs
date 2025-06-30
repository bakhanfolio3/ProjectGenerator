using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProjectName.Api.Controllers;

[ApiVersion("1")]
[Route("v{version:apiVersion}/[controller]")]
[ApiController]
//[Authorize]
public class UserRoleController : BaseApiController<UserRoleController>
{
    public UserRoleController(IMediator mediator, ILogger<UserRoleController> logger) : base(mediator, logger)
    {
    }

    [HttpGet]
    public IActionResult GetUserRoles()
    {
        return Ok("Roles");
    }
}
