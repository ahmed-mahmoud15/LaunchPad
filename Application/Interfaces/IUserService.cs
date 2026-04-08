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
        Task<Result<UserProfileDto>> GetUserProfileAsync(int id);
        Task<Result<PagedResponse<UserDto>>> GetAllAsync(PagedRequest request);
        //Task<Result<UserDto>> CreateUserAsync(CreateUserDto dto);
        Task<Result> UpdateUserAsync(int id, UpdateUserDto dto);
        Task<Result> DeleteUserAsync(int id);
    }
}
