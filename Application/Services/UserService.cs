using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unit;
        public UserService(IUnitOfWork unit)
        {
            this.unit = unit;
        }

        public async Task<Result<IEnumerable<UserDto>>> GetAllAsync()
        {
            var users = await unit.Users.GetAllAsync();
            IList<UserDto> result = new List<UserDto>();
            foreach(var user in users)
            {
                var dto = new UserDto
                {
                    Address = user.Address,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    JoinDate = user.JoinDate,
                    PhoneNumber = user.PhoneNumber,
                    Role = user.Role.ToString()
                };
                result.Add(dto);
            }
            return Result<IEnumerable<UserDto>>.Success(result);
        }

        public async Task<Result<UserDto>> GetByIdAsync(int id)
        {
            var user = await unit.Users.GetByIdAsync(id);
            if (user is null)
            {
                return Result<UserDto>.Failure("User Not Found");
            }
            var dto = new UserDto
            {
                Address = user.Address,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = id,
                JoinDate = user.JoinDate,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role.ToString()
            };
            return Result<UserDto>.Success(dto);
        }
    }
}
