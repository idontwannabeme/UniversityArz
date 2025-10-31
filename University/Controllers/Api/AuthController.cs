using Microsoft.AspNetCore.Mvc;
using UniversityInformationSystem.Models;
using UniversityInformationSystem.Services;

namespace UniversityInformationSystem.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { message = "Email и пароль обязательны" });
            }

            var user = await _userService.Authenticate(request.Email, request.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Неверный email или пароль" });
            }

            return Ok(new
            {
                user.Id,
                user.FullName,
                user.Email,
                user.StudentId,
                user.Role,
                FacultyName = user.Faculty?.Name
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { message = "Email и пароль обязательны" });
            }

            if (request.Password.Length < 6)
            {
                return BadRequest(new { message = "Пароль должен содержать минимум 6 символов" });
            }

            // Проверяем, нет ли уже пользователя с таким email или studentId
            var userExists = await _userService.UserExists(request.Email, request.StudentId);
            if (userExists)
            {
                return BadRequest(new { message = "Пользователь с таким email или студенческим билетом уже существует" });
            }

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                StudentId = request.StudentId,
                FacultyId = request.FacultyId,
                Password = request.Password,
                Role = UserRole.Student
            };

            var createdUser = await _userService.Register(user);

            if (createdUser == null)
            {
                return BadRequest(new { message = "Ошибка при регистрации пользователя" });
            }

            return Ok(new
            {
                message = "Регистрация выполнена успешно",
                user = new
                {
                    createdUser.Id,
                    createdUser.FullName,
                    createdUser.Email,
                    createdUser.StudentId,
                    createdUser.Role
                }
            });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
        public int FacultyId { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}