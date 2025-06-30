using Asp.Versioning;
using ProjectName.Application.Features.Auth.Command.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ProjectName.Api.Controllers;

[ApiVersion("1")]
[Route("v{version:apiVersion}/[controller]")]
[ApiController]
//[Authorize]
public class AuthenticationController : BaseApiController<AuthenticationController>
{
    public AuthenticationController(IMediator mediator, ILogger<AuthenticationController> logger) 
        : base(mediator, logger)
    {
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginAuthCommand request)
    {
        request.IpAddress = GenerateIPAddress();
        var result = await Mediator.Send(request);
        return Ok(result);
    }

    private string GenerateIPAddress()
    {
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];
        else
            return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }
}
