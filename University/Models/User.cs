using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using University.Models;

namespace UniversityInformationSystem.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string StudentId { get; set; } = string.Empty;

        [Required]
        public int FacultyId { get; set; }

        [ForeignKey("FacultyId")]
        public Faculty? Faculty { get; set; }

        [Required]
        public string Password { get; set; } = string.Empty; // Простой пароль для разработки

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLoginAt { get; set; }

        public UserRole Role { get; set; } = UserRole.Student;
    }

    public enum UserRole
    {
        Student,
        Teacher,
        Admin
    }
}

//