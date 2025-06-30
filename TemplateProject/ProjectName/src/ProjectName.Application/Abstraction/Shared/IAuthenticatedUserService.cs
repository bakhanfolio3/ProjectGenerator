namespace ProjectName.Application.Abstraction.Shared;

public interface IAuthenticatedUserService
{
    string UserId { get; }
    public string Username { get; }
}