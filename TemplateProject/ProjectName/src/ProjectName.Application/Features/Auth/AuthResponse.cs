using ProjectName.Application.Abstraction.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ProjectName.Application.Features.Auth;
public class AuthResponse : IResponse
{
    //public string Token { get; set; } = string.Empty;
    //public string RefreshToken { get; set; } = string.Empty;
    //public DateTime Expires { get; set; }


    public required string Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public string? Role { get; set; }
    public bool IsVerified { get; set; }
    public string? JWToken { get; set; }
    public DateTime IssuedOn { get; set; }
    public DateTime ExpiresOn { get; set; }

    [JsonIgnore]
    public string? RefreshToken { get; set; }
}
