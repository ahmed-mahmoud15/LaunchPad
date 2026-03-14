using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Auth;
using Domain.Common;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result<AuthResponseDto>> LoginAsync(LoginDto dto);
        Task<Result<AuthResponseDto>> RegisterAsync(RegisterDto dto);
    }
}
