using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using University.Models;

namespace UniversityInformationSystem.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(50)]
        public string? MiddleName { get; set; }

        [Required]
        [StringLength(20)]
        public string StudentId { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [Required]
        public int FacultyId { get; set; }

        [ForeignKey("FacultyId")]
        public Faculty? Faculty { get; set; }

        [Required]
        [Range(1, 6)]
        public int Course { get; set; }

        [Required]
        [StringLength(20)]
        public string Group { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;

        public StudentStatus Status { get; set; } = StudentStatus.Active;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }

    public enum StudentStatus
    {
        Active,
        AcademicLeave,
        Expelled
    }
}