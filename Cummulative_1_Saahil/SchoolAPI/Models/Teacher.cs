using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        [MaxLength(200)]
        public string Subject { get; set; }

        /// <summary>
        /// Navigation property for the courses taught by this teacher.
        /// </summary>
        public List<Course> Courses { get; set; }
    }
}
