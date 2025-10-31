using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using University.Models;

namespace UniversityInformationSystem.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Code { get; set; }

        [Required]
        public int Credits { get; set; }

        [Required]
        public int FacultyId { get; set; }

        public Faculty Faculty { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Range(1, 6)]
        public int Semester { get; set; }
    }
}