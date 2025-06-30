using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog.Core;

namespace ProjectName.Api.Controllers;

public abstract class BaseController : ControllerBase
{
    public BaseController(IMediator mediator)
    {
        Mediator = mediator;
    }

    public IMediator Mediator { get; }
}

public abstract class BaseApiController<T>: BaseController
{
    public BaseApiController(IMediator mediator, ILogger<T> logger)
        : base(mediator)
    {
        Logger = logger;
    }

    
    public ILogger<T> Logger { get; }
}
