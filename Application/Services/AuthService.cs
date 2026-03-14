using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs.Auth;
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUnitOfWork unit;
        private readonly IConfiguration config;

        public AuthService(IUnitOfWork unit, IConfiguration config)
        {
            this.unit = unit;
            this.config = config;
        }

        public async Task<Result<AuthResponseDto>> LoginAsync(LoginDto dto)
        {
            var user = await unit.Users.FindAsync(u => u.Email == dto.Email);
            
            if(user is null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHashed)){
                return Result<AuthResponseDto>.Unauthorized("Invalid Email or Password");
            }

            if (!user.IsActive)
            {
                return Result<AuthResponseDto>.Forbidden("Account is Deactivated");
            }

            var token = GenerateToken(user);
            return Result<AuthResponseDto>.Ok(MapToResponse(user, token));
        }

        public async Task<Result<AuthResponseDto>> RegisterAsync(RegisterDto dto)
        {
            var userFromDb = await unit.Users.FindAsync(u => u.Email == dto.Email);

            if(userFromDb is not null)
            {
                return Result<AuthResponseDto>.Conflict("This Email is Already Registered");
            }

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Address = dto.Address,
                Role = UserRole.User,
                JoinDate = DateOnly.FromDateTime(DateTime.UtcNow),
                PasswordHashed = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                PhoneNumber = dto.PhoneNumber,
                IsActive = true
            };

            await unit.Users.AddAsync(user);
            await unit.SaveChangesAsync();

            var token = GenerateToken(user);
            return Result<AuthResponseDto>.Created(MapToResponse(user, token));
        }

        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));

            var token = new JwtSecurityToken(
                    issuer: config["Jwt:Issuer"],
                    audience: config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(double.Parse(config["Jwt:ExpiryDays"]!)),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static AuthResponseDto MapToResponse(User user, string token)
        {
            return new AuthResponseDto()
            {
                Token = token,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
                Role = user.Role.ToString()
            };
        }
    }
}
