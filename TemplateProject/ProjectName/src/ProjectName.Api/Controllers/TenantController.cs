using Asp.Versioning;
using ProjectName.Application.Features.Tenant;
using ProjectName.Application.Abstraction.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ProjectName.Application.Features.Tenant.Query.List;
using ProjectName.Application.Features.Tenant.Command.Create;
using Microsoft.AspNetCore.Authorization;
using ProjectName.Application.Common.Responses;
using ProjectName.Application.Common.Query;
using ProjectName.Application.Features.Tenant.Command.Update;
using System.ComponentModel.DataAnnotations;
using ProjectName.Application.Abstraction.Messagings;
using ProjectName.Application.Common.Command;

namespace ProjectName.Api.Controllers;

[ApiVersion("1")]
[Route("v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(Roles = "SuperAdmin,Admin")]
public class TenantController : BaseApiController<TenantController>
{
    public TenantController(IMediator mediator, ILogger<TenantController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Get Tenant List
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IPaginatedResult<TenantResponse>>> GetTenants([FromQuery] ListTenantQuery request)
    {
        var result = await Mediator.Send(request);
        return Ok(result);
    }

    /// <summary>
    /// Get Tenant List
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<IPaginatedResult<TenantResponse>>> GetTenantById([FromRoute] int id)
    {
        var result = await Mediator.Send(new GetByIdQuery<TenantResponse>(id));
        return Ok(result);
    }

    /// <summary>
    /// Get Tenant List
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("GetNameValueList")]
    public async Task<ActionResult<IResult<NameValueResponse<int>>>> GetNameValueList([FromQuery] NVListQuery<int> request)
    {
        var result = await Mediator.Send(request);
        return Ok(result);
    }

    /// <summary>
    /// CreateTenant Tenant
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<IResult<TenantResponse>>> CreateTenant([FromBody] CreateTenantCommand request)
    {
        var result = await Mediator.Send(request);
        return Ok(result);
    }

    /// <summary>
    /// CreateTenant Tenant
    /// </summary>
    /// <param name="request"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<IResult<TenantResponse>>> UpdateTenant([FromBody] UpdateTenantCommand request, [FromRoute] [Required] int id)
    {
        request.Id = id;
        var result = await Mediator.Send(request);
        return Ok(result);
    }

    /// <summary>
    /// CreateTenant Tenant
    /// </summary>
    /// <param name="request"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<IResult<TenantResponse>>> DeleteTenant([FromBody] DeleteCommand request, [FromRoute][Required] int id)
    {
        request.Id = id;
        var result = await Mediator.Send(request);
        return Ok(result);
    }
}
