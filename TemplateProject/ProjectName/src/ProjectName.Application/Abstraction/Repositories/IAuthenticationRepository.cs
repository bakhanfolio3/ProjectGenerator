
using ProjectName.Application.Abstraction.Responses;
using ProjectName.Application.DTOs.Identity;
using ProjectName.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.Repositories;
public interface IAuthenticationRepository 
{
    Task<IResult<TokenResponse>> LoginAsync(TokenRequest request, string ipAddress);
}
