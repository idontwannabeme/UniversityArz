using Microsoft.EntityFrameworkCore;
using University.Models;
using UniversityInformationSystem.Models;

namespace UniversityInformationSystem.Data
{
    public class UniversityContext : DbContext
    {
        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Заполнение начальными данными для Faculty
            modelBuilder.Entity<Faculty>().HasData(
                new Faculty
                {
                    Id = 1,
                    Name = "Информатики и вычислительной техники",
                    Dean = "Иванов А.С.",
                    Email = "dean@cs.university.ru",
                    Phone = "+7 (495) 111-11-11",
                    Description = "Факультет готовит специалистов в области информационных технологий и компьютерных наук"
                },
                new Faculty
                {
                    Id = 2,
                    Name = "Экономический",
                    Dean = "Петрова М.В.",
                    Email = "dean@eco.university.ru",
                    Phone = "+7 (495) 111-11-12",
                    Description = "Факультет экономики и управления"
                },
                new Faculty
                {
                    Id = 3,
                    Name = "Юридический",
                    Dean = "Сидоров П.К.",
                    Email = "dean@law.university.ru",
                    Phone = "+7 (495) 111-11-13",
                    Description = "Факультет права и юриспруденции"
                },
                new Faculty
                {
                    Id = 4,
                    Name = "Филологический",
                    Dean = "Козлова А.В.",
                    Email = "dean@phil.university.ru",
                    Phone = "+7 (495) 111-11-14",
                    Description = "Факультет филологии и лингвистики"
                }
            );

            // Добавляем тестового пользователя
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FullName = "Администратор Системы",
                    Email = "admin@university.ru",
                    StudentId = "ADMIN001",
                    FacultyId = 1,
                    Password = "admin123", // Пароль в открытом виде (только для разработки!)
                    Role = UserRole.Admin,
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = 2,
                    FullName = "Иванов Алексей Петрович",
                    Email = "ivanov@student.university.ru",
                    StudentId = "ST2021001",
                    FacultyId = 1,
                    Password = "student123",
                    Role = UserRole.Student,
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Остальные данные (Students, Teachers, Courses)...
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    FirstName = "Алексей",
                    LastName = "Иванов",
                    MiddleName = "Петрович",
                    StudentId = "ST2021001",
                    Email = "ivanov@student.university.ru",
                    Phone = "+7 (915) 123-45-67",
                    FacultyId = 1,
                    Course = 3,
                    Group = "ИВТ-31",
                    EnrollmentDate = new DateTime(2021, 9, 1),
                    CreatedAt = new DateTime(2021, 9, 1)
                },
                new Student
                {
                    Id = 2,
                    FirstName = "Мария",
                    LastName = "Петрова",
                    MiddleName = "Сергеевна",
                    StudentId = "ST2021002",
                    Email = "petrova@student.university.ru",
                    Phone = "+7 (915) 123-45-68",
                    FacultyId = 2,
                    Course = 2,
                    Group = "ЭК-22",
                    EnrollmentDate = new DateTime(2021, 9, 1),
                    CreatedAt = new DateTime(2021, 9, 1)
                }
            );

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher
                {
                    Id = 1,
                    FirstName = "Александр",
                    LastName = "Иванов",
                    MiddleName = "Сергеевич",
                    Email = "ivanov@university.ru",
                    Phone = "+7 (495) 222-22-22",
                    FacultyId = 1,
                    Position = "Профессор",
                    AcademicDegree = "Доктор технических наук"
                }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    Name = "Базы данных",
                    Code = "DB101",
                    Credits = 4,
                    FacultyId = 1,
                    Description = "Основы проектирования и работы с базами данных",
                    Semester = 3
                }
            );
        }
    }
}