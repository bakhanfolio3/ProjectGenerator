using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ProjectName.Application.DTOs.Identity;

public class TokenResponse
{
    public string Id { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string? Role { get; set; }
    public required string Email { get; set; }
    public bool IsVerified { get; set; }
    public string? JWToken { get; set; }
    public DateTime IssuedOn { get; set; }
    public DateTime ExpiresOn { get; set; }

    [JsonIgnore]
    public string? RefreshToken { get; set; }
}