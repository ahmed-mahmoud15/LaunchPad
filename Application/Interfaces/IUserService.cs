using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.User;
using Domain.Common;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<Result<UserDto>> GetByIdAsync(int id);
        Task<Result<IEnumerable<UserDto>>> GetAllAsync();
        Task<Result<UserDto>> CreateUserAsync(CreateUserDto dto);
        Task<Result> UpdateUserAsync(int id, UpdateUserDto dto);
        Task<Result> DeleteUserAsync(int id);
    }
}
