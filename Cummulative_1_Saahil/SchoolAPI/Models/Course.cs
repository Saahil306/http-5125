using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        [MaxLength(200)]
        public string CourseName { get; set; }

        /// <summary>
        /// Foreign key linking to the Teacher entity.
        /// </summary>
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }

        /// <summary>
        /// Navigation property to associate a course with a teacher.
        /// </summary>
        public Teacher Teacher { get; set; }
    }
}
