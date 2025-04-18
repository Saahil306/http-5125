using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public StudentAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all students from the database.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        /// <summary>
        /// Retrieves a single student by ID.
        /// </summary>
        /// <param name="id">The ID of the student</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound(new { message = "Error: Student not found.", studentId = id });
            }
            return student;
        }

        /// <summary>
        /// Creates a new student in the database.
        /// Ensures the student's name is not empty.
        /// </summary>
        /// <param name="student">The student object to create</param>
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.Name))
            {
                return BadRequest(new { message = "Error: Student name cannot be empty." });
            }

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.StudentId }, student);
        }

        /// <summary>
        /// Deletes a student from the database.
        /// Returns an error message if the student does not exist.
        /// </summary>
        /// <param name="id">The ID of the student to delete</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound(new { message = "Error: Student not found.", studentId = id });
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Student successfully deleted.", studentId = id });
        }

        /// <summary>
        /// Updates an existing student in the database.
        /// </summary>
        /// <param name="id">The ID of the student to update</param>
        /// <param name="updatedStudent">The updated student object</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student updatedStudent)
        {
            // Check if the student ID in the URL matches the ID in the body
            if (id != updatedStudent.StudentId)
            {
                return BadRequest("Student ID mismatch");
            }

            // Find the existing student in the database
            var existingStudent = await _context.Students.FindAsync(id);
            if (existingStudent == null)
            {
                return NotFound(new { message = "Error: Student not found.", studentId = id });
            }

            // Validate the updated student data
            if (string.IsNullOrWhiteSpace(updatedStudent.Name))
            {
                return BadRequest(new { message = "Error: Student name cannot be empty." });
            }

            // Update properties of the existing student with the new data
            existingStudent.Name = updatedStudent.Name;
            existingStudent.EnrollmentDate = updatedStudent.EnrollmentDate;

            try
            {
                // Save changes to the database
                await _context.SaveChangesAsync();
                return Ok(new { message = "Student successfully updated.", studentId = id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error updating student: " + ex.Message);
            }
        }

    }
}
