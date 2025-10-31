using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityInformationSystem.Models;
using UniversityInformationSystem.Data;
using University.Models;

namespace UniversityInformationSystem.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultiesController : ControllerBase
    {
        private readonly UniversityContext _context;

        public FacultiesController(UniversityContext context)
        {
            _context = context;
        }

        // GET: api/faculties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Faculty>>> GetFaculties()
        {
            return await _context.Faculties.ToListAsync();
        }

        // GET: api/faculties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Faculty>> GetFaculty(int id)
        {
            var faculty = await _context.Faculties.FindAsync(id);

            if (faculty == null)
            {
                return NotFound();
            }

            return faculty;
        }

        // GET: api/faculties/5/students
        [HttpGet("{id}/students")]
        public async Task<ActionResult<IEnumerable<Student>>> GetFacultyStudents(int id)
        {
            var students = await _context.Students
                .Where(s => s.FacultyId == id)
                .Include(s => s.Faculty)
                .ToListAsync();

            return Ok(students);
        }

        // GET: api/faculties/5/statistics
        [HttpGet("{id}/statistics")]
        public async Task<ActionResult<object>> GetFacultyStatistics(int id)
        {
            var totalStudents = await _context.Students.CountAsync(s => s.FacultyId == id);
            var activeStudents = await _context.Students.CountAsync(s => s.FacultyId == id && s.Status == StudentStatus.Active);
            var totalTeachers = await _context.Teachers.CountAsync(t => t.FacultyId == id);
            var totalCourses = await _context.Courses.CountAsync(c => c.FacultyId == id);

            return Ok(new
            {
                TotalStudents = totalStudents,
                ActiveStudents = activeStudents,
                TotalTeachers = totalTeachers,
                TotalCourses = totalCourses
            });
        }
    }
}