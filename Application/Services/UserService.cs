using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.User;
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
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

        public async Task<Result<UserDto>> CreateUserAsync(CreateUserDto dto)
        {
            var userFromDb = await unit.Users.FindAsync(u => u.Email == dto.Email);
            
            if(userFromDb != null)
            {
                return Result<UserDto>.Conflict("User With This Email Already Exists!");
            }

            var user = new User()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Address = dto.Address,
                Role = UserRole.User,
                JoinDate = DateOnly.FromDateTime(DateTime.UtcNow),
                PasswordHashed = dto.Password,
                PhoneNumber = dto.PhoneNumber,
                IsActive = true
            };
            await unit.Users.AddAsync(user);
            await unit.SaveChangesAsync();

            var userDto = new UserDto() {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Address = user.Address,
                PhoneNumber= user.PhoneNumber,
                Id = user.Id,
                JoinDate = user.JoinDate,
                Role = user.Role.ToString()
            };
            return Result<UserDto>.Created(userDto);
        }

        public Task<Result> DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
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
            return Result<IEnumerable<UserDto>>.Ok(result);
        }

        public async Task<Result<UserDto>> GetByIdAsync(int id)
        {
            var user = await unit.Users.GetByIdAsync(id);
            if (user is null)
            {
                return Result<UserDto>.NotFound("User Not Found");
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
            return Result<UserDto>.Ok(dto);
        }

        public Task<Result> UpdateUserAsync(UpdateUserDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
