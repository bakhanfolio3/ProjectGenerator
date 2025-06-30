//using ProjectName.Application.Common.Responses;
//using ProjectName.Domain.Entities.Enums;
//using MediatR;

//namespace ProjectName.Application.Features.Users.GetAllUsers;

//public record GetAllUsersRequest
//    (string? FirstName = null, string? LastName = null, UserType? UserType = null, string? Team = null, int CurrentPage = 1, int PageSize = 15) : IRequest<PaginatedResult<GetUserResponse>>;