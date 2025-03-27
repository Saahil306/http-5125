using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Models;
using System.Text.RegularExpressions;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public TeacherAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all teachers from the database.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        {
            return await _context.Teachers.ToListAsync();
        }

        /// <summary>
        /// Retrieves a single teacher by ID.
        /// Includes error handling if the teacher does not exist.
        /// </summary>
        /// <param name="id">The ID of the teacher</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
            var teacher = await _context.Teachers
                .Include(t => t.Courses)
                .FirstOrDefaultAsync(t => t.TeacherId == id);

            if (teacher == null)
            {
                return NotFound(new { message = "Error: Teacher not found.", teacherId = id });
            }

            return teacher;
        }

        /// <summary>
        /// Creates a new teacher in the database.
        /// Includes error handling for various validations.
        /// </summary>
        /// <param name="teacher">The teacher object to create</param>
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {
            if (string.IsNullOrWhiteSpace(teacher.Name))
            {
                return BadRequest(new { message = "Error: Teacher name cannot be empty." });
            }

            if (teacher.HireDate > DateTime.Now)
            {
                return BadRequest(new { message = "Error: Hire date cannot be in the future." });
            }

            if (!Regex.IsMatch(teacher.EmployeeNumber, @"^T\d+$"))
            {
                return BadRequest(new { message = "Error: Employee Number must start with 'T' followed by digits." });
            }

            if (await _context.Teachers.AnyAsync(t => t.EmployeeNumber == teacher.EmployeeNumber))
            {
                return BadRequest(new { message = "Error: Employee Number already exists." });
            }

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTeacher), new { id = teacher.TeacherId }, teacher);
        }

        /// <summary>
        /// Deletes a teacher from the database.
        /// Returns an error message if the teacher does not exist.
        /// </summary>
        /// <param name="id">The ID of the teacher to delete</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound(new { message = "Error: Teacher not found.", teacherId = id });
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Teacher successfully deleted.", teacherId = id });
        }
    }
}
