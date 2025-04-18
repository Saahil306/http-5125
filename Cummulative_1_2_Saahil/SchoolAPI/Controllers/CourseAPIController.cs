using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public CourseAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all courses from the database.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        /// <summary>
        /// Retrieves a single course by ID.
        /// </summary>
        /// <param name="id">The ID of the course</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound(new { message = "Error: Course not found.", courseId = id });
            }
            return course;
        }

        /// <summary>
        /// Creates a new course in the database.
        /// Ensures the course name is not empty.
        /// </summary>
        /// <param name="course">The course object to create</param>
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            if (string.IsNullOrWhiteSpace(course.CourseName))
            {
                return BadRequest(new { message = "Error: Course name cannot be empty." });
            }

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourse), new { id = course.CourseId }, course);
        }

        /// <summary>
        /// Deletes a course from the database.
        /// Returns an error message if the course does not exist.
        /// </summary>
        /// <param name="id">The ID of the course to delete</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound(new { message = "Error: Course not found.", courseId = id });
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Course successfully deleted.", courseId = id });
        }

        /// <summary>
        /// Updates an existing course.
        /// </summary>
        /// <param name="id">The ID of the course to update.</param>
        /// <param name="course">The updated course object.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] Course course)
        {
            if (id != course.CourseId)
            {
                return BadRequest("Course ID mismatch.");
            }

            var existingCourse = await _context.Courses.FindAsync(id);
            if (existingCourse == null)
            {
                return NotFound(new { message = "Error: Course not found.", courseId = id });
            }

            // Validate course name
            if (string.IsNullOrWhiteSpace(course.CourseName))
            {
                return BadRequest(new { message = "Error: Course name cannot be empty." });
            }

            // Update course properties
            existingCourse.CourseName = course.CourseName;
            existingCourse.TeacherId = course.TeacherId; // Assuming TeacherId can be updated

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Course updated successfully", courseId = id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error updating course: " + ex.Message);
            }
        }
    }
}
