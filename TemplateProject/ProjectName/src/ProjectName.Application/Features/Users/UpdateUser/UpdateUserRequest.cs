//using ProjectName.Application.Abstraction.Responses;
//using ProjectName.Domain.Entities.Common;
//using ProjectName.Domain.Entities.Enums;
//using MediatR;
//using System.Text.Json.Serialization;

//namespace ProjectName.Application.Features.Users.UpdateUser;

//public record UpdateUserRequest : IRequest<IResult<GetUserResponse>>
//{
//    [JsonIgnore]
//    public int Id { get; init; }
    
//    public string FirstName { get; init; } = null!;

//    public string? LastName { get; init; }
//    //public int? Age { get; init; }
//    //public string Individuality { get; init; } = null!;
//    public UserType UserType { get; init; }

//    //public string? Team { get; init; }
//}