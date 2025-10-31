using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using University.Models;
using UniversityInformationSystem.Models;

namespace University.Models
{
    public class Faculty
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)] // Убрали Required
        public string? Description { get; set; }

        [StringLength(50)] // Убрали Required
        public string? Dean { get; set; }

        [EmailAddress] // Убрали Required
        public string? Email { get; set; }

        [Phone] // Убрали Required
        public string? Phone { get; set; }

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
        public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}