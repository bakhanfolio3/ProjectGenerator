//using Ardalis.Result.AspNetCore;
//using ProjectName.Application.Features.Users.CreateUser;
//using ProjectName.Application.Features.Users.DeleteUser;
//using ProjectName.Application.Features.Users.GetAllUsers;
//using ProjectName.Application.Features.Users.GetUserById;
//using ProjectName.Application.Features.Users.UpdateUser;
//using ProjectName.Domain.Entities.Common;
//using MediatR;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Routing;

//namespace ProjectName.Api.Endpoints;

//public static class UserEndpoints
//{
//    public static void MapHeroEndpoints(this IEndpointRouteBuilder builder)
//    {
//        var group = builder.MapGroup("api/User")
//            .WithTags("User");
        
//        group.MapGet("/", async (IMediator mediator, [AsParameters] GetAllUsersRequest request) =>
//        {
//            var result = await mediator.Send(request);
//            return result;
//        });

//        group.MapGet("{id}", async (IMediator mediator, int id) =>
//        {
//            var result = await mediator.Send(new GetUserByIdRequest(id));
//            return result.ToMinimalApiResult();
//        });

//        group.MapPost("/", async (IMediator mediator, CreateUserRequest request) =>
//        {
//            var result = await mediator.Send(request);
//            return result.ToMinimalApiResult();
//        });

//        group.MapPut("{id}", async (IMediator mediator, int id, UpdateUserRequest request) =>
//        {
//            var result = await mediator.Send(request with { Id = id });
//            return result.ToMinimalApiResult();
//        });

//        group.MapDelete("{id}", async (IMediator mediator, int id) =>
//        {
//            var result = await mediator.Send(new DeleteUserRequest(id));
//            return result.ToMinimalApiResult();
//        });
//    }
//}