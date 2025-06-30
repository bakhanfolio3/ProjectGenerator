//using ProjectName.Application.Common.Responses;
//using ProjectName.Application.Abstraction.Responses;
//using ProjectName.Domain.Entities.Enums;
//using MediatR;

//namespace ProjectName.Application.Features.Users.CreateUser;

//public record CreateUserRequest : IRequest<IResult<GetUserResponse>>
//{
//    public string FirstName { get; init; } = null!;

//    public string? LastName { get; init; }
//    //public int? Age { get; init; }
//    //public string Individuality { get; init; } = null!;
//    public UserType UserType { get; init; }

//    public string? Team { get; init; }
//}