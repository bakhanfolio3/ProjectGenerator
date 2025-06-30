namespace ProjectName.Application.DTOs.Identity;

public class TokenRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public int TenantId { get; set; }
}