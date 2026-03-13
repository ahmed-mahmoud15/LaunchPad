using System.Reflection.Metadata.Ecma335;
using Application.DTOs.User;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService) {
            this.userService = userService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            var result = await userService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await userService.GetAllAsync();
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            var result =  await userService.CreateUserAsync(dto);
            return StatusCode(result.StatusCode, result.IsSuccess ? result.Value : result.ErrorMessage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
        {
            var result = await userService.UpdateUserAsync(id, dto);
            if (!result.IsSuccess) {
                return NotFound(result.ErrorMessage);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await userService.DeleteUserAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return NoContent();
        }
    }
}
