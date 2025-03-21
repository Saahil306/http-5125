using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Models;

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
                .Include(t => t.Courses) // Include related courses
                .FirstOrDefaultAsync(t => t.TeacherId == id);

            if (teacher == null)
            {
                return NotFound(new { message = "Error: Teacher not found.", teacherId = id });
            }

            return teacher;
        }

        /// <summary>
        /// Creates a new teacher in the database.
        /// </summary>
        /// <param name="teacher">The teacher object to create</param>
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTeacher), new { id = teacher.TeacherId }, teacher);
        }

        /// <summary>
        /// Updates an existing teacher.
        /// </summary>
        /// <param name="id">The ID of the teacher to update</param>
        /// <param name="teacher">The updated teacher object</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, Teacher teacher)
        {
            if (id != teacher.TeacherId)
            {
                return BadRequest();
            }

            _context.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Teachers.Any(e => e.TeacherId == id))
                {
                    return NotFound(new { message = "Error: Teacher not found during update.", teacherId = id });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a teacher from the database.
        /// </summary>
        /// <param name="id">The ID of the teacher to delete</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound(new { message = "Error: Teacher not found during delete.", teacherId = id });
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
