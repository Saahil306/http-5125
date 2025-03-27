using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models
{
    /// <summary>
    /// Represents a student in the school database.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// The unique ID of the student.
        /// </summary>
        [Key]
        public int StudentId { get; set; }

        /// <summary>
        /// The name of the student.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// The date the student enrolled.
        /// </summary>
        [Required]
        public DateTime EnrollmentDate { get; set; }
    }
}
