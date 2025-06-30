using ProjectName.Application.Features.Users;
using ProjectName.Application.Abstraction.Responses;
using ProjectName.Domain.Entities.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectName.Application.Abstraction.Messagings;
using ProjectName.Application.Features.Auth;
using System.Text.Json.Serialization;

namespace ProjectName.Application.Features.Auth.Command.Login;
public class LoginAuthCommand : ICommand<AuthResponse>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public int TenantId { get; set; }
    [JsonIgnore]
    public string IpAddress { get; set; } = string.Empty;
}