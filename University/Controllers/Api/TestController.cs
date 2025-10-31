using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityInformationSystem.Data;
using UniversityInformationSystem.Models;

namespace UniversityInformationSystem.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly UniversityContext _context;

        public TestController(UniversityContext context)
        {
            _context = context;
        }

        [HttpGet("database")]
        public async Task<IActionResult> CheckDatabase()
        {
            try
            {
                // Проверяем подключение к базе
                var canConnect = await _context.Database.CanConnectAsync();

                // Проверяем существование таблиц
                var studentsCount = await _context.Students.CountAsync();
                var facultiesCount = await _context.Faculties.CountAsync();

                return Ok(new
                {
                    DatabaseConnected = canConnect,
                    StudentsTableExists = true,
                    StudentsCount = studentsCount,
                    FacultiesCount = facultiesCount,
                    Message = "✅ База данных работает корректно"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    DatabaseConnected = false,
                    StudentsTableExists = false,
                    Error = ex.Message,
                    Message = "❌ Ошибка подключения к базе данных"
                });
            }
        }

        [HttpPost("add-test-student")]
        public async Task<IActionResult> AddTestStudent()
        {
            try
            {
                var testStudent = new Student
                {
                    FirstName = "Тестовый",
                    LastName = "Студент",
                    StudentId = "TEST001",
                    Email = "test@university.ru",
                    FacultyId = 1,
                    Course = 1,
                    Group = "ТЕСТ-01",
                    EnrollmentDate = DateTime.Now,
                    Status = StudentStatus.Active,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Students.Add(testStudent);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    Success = true,
                    Message = "✅ Тестовый студент добавлен",
                    StudentId = testStudent.Id
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "❌ Ошибка добавления студента",
                    Error = ex.Message
                });
            }
        }
    }
}