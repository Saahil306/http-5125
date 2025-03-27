using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    public class TeacherController : Controller
    {
        private readonly SchoolDbContext _context;

        public TeacherController(SchoolDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Displays a list of all teachers.
        /// </summary>
        public async Task<IActionResult> List()
        {
            var teachers = await _context.Teachers.ToListAsync();
            return View(teachers);
        }

        /// <summary>
        /// Displays details of a specific teacher, including courses taught.
        /// </summary>
        /// <param name="id">The ID of the teacher</param>
        public async Task<IActionResult> Show(int id)
        {
            var teacher = await _context.Teachers
                .Include(t => t.Courses)
                .FirstOrDefaultAsync(t => t.TeacherId == id);

            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        /// <summary>
        /// Displays the form to add a new teacher.
        /// </summary>
        public IActionResult New()
        {
            return View();
        }

        /// <summary>
        /// Handles the submission of the new teacher form.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(Teacher teacher)
        {
            if (string.IsNullOrWhiteSpace(teacher.Name))
            {
                ViewData["ErrorMessage"] = "Error: Teacher name cannot be empty.";
                return View("New");
            }

            if (teacher.HireDate > DateTime.Now)
            {
                ViewData["ErrorMessage"] = "Error: Hire date cannot be in the future.";
                return View("New");
            }

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

        /// <summary>
        /// Displays the confirmation page before deleting a teacher.
        /// </summary>
        /// <param name="id">The ID of the teacher</param>
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        /// <summary>
        /// Handles the deletion of a teacher after confirmation.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }
    }
}
