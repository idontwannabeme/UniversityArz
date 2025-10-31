using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityInformationSystem.Data;
using UniversityInformationSystem.Models;

namespace UniversityInformationSystem.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly UniversityContext _context;

        public StudentsController(UniversityContext context)
        {
            _context = context;
        }

        // GET: api/students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetStudents()
        {
            try
            {
                var students = await _context.Students
                    .Include(s => s.Faculty)
                    .OrderBy(s => s.LastName)
                    .ThenBy(s => s.FirstName)
                    .Select(s => new
                    {
                        s.Id,
                        s.StudentId,
                        s.FirstName,
                        s.LastName,
                        s.MiddleName,
                        s.Email,
                        s.Phone,
                        Faculty = new { s.Faculty.Id, s.Faculty.Name },
                        s.FacultyId,
                        s.Course,
                        s.Group,
                        s.Status,
                        s.EnrollmentDate,
                        s.CreatedAt
                    })
                    .ToListAsync();

                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // GET: api/students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            try
            {
                var student = await _context.Students
                    .Include(s => s.Faculty)
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (student == null)
                {
                    return NotFound(new { error = "Студент не найден" });
                }

                return student;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // POST: api/students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent([FromBody] Student student)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Проверяем, что факультет существует
                var facultyExists = await _context.Faculties.AnyAsync(f => f.Id == student.FacultyId);
                if (!facultyExists)
                {
                    return BadRequest(new { error = "Указанный факультет не существует" });
                }

                student.CreatedAt = DateTime.UtcNow;
                student.UpdatedAt = null;

                _context.Students.Add(student);
                await _context.SaveChangesAsync();

                // Загружаем связанные данные для ответа
                await _context.Entry(student)
                    .Reference(s => s.Faculty)
                    .LoadAsync();

                return CreatedAtAction("GetStudent", new { id = student.Id }, student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Ошибка при создании студента: {ex.Message}" });
            }
        }

        // PUT: api/students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, [FromBody] Student student)
        {
            try
            {
                if (id != student.Id)
                {
                    return BadRequest(new { error = "ID в URL и теле запроса не совпадают" });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingStudent = await _context.Students.FindAsync(id);
                if (existingStudent == null)
                {
                    return NotFound(new { error = "Студент не найден" });
                }

                // Проверяем факультет
                var facultyExists = await _context.Faculties.AnyAsync(f => f.Id == student.FacultyId);
                if (!facultyExists)
                {
                    return BadRequest(new { error = "Указанный факультет не существует" });
                }

                // Обновляем поля
                existingStudent.FirstName = student.FirstName;
                existingStudent.LastName = student.LastName;
                existingStudent.MiddleName = student.MiddleName;
                existingStudent.StudentId = student.StudentId;
                existingStudent.Email = student.Email;
                existingStudent.Phone = student.Phone;
                existingStudent.FacultyId = student.FacultyId;
                existingStudent.Course = student.Course;
                existingStudent.Group = student.Group;
                existingStudent.Status = student.Status;
                existingStudent.EnrollmentDate = student.EnrollmentDate;
                existingStudent.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Ошибка при обновлении студента: {ex.Message}" });
            }
        }

        // DELETE: api/students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var student = await _context.Students.FindAsync(id);
                if (student == null)
                {
                    return NotFound(new { error = "Студент не найден" });
                }

                _context.Students.Remove(student);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Ошибка при удалении студента: {ex.Message}" });
            }
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}