using ProjectName.Application.Abstraction.Responses;
using ProjectName.Domain.Entities.Common;
using ProjectName.Domain.Entities.Enums;

namespace ProjectName.Application.Features.Users;

public record GetUserResponse: IResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string? Nickname { get; init; }

    public int? Age { get; init; }

    public string Individuality { get; init; } = null!;
    public UserType? UserType { get; init; }

    public string? Team { get; init; }
}