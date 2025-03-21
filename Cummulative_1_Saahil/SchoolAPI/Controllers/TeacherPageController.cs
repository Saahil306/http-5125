using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    public class TeacherPageController : Controller
    {
        private readonly SchoolDbContext _context;

        public TeacherPageController(SchoolDbContext context)
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
                .Include(t => t.Courses) // ✅ Include courses taught by this teacher
                .FirstOrDefaultAsync(t => t.TeacherId == id);

            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        /// <summary>
        /// Searches for teachers within a specified hire date range.
        /// </summary>
        /// <param name="startDate">Start date of hire range</param>
        /// <param name="endDate">End date of hire range</param>
        public async Task<IActionResult> Search(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null)
            {
                ViewData["ErrorMessage"] = "Please select both start and end dates.";
                return View("List", await _context.Teachers.ToListAsync());
            }

            var teachers = await _context.Teachers
                .Where(t => t.HireDate >= startDate && t.HireDate <= endDate)
                .ToListAsync();

            if (teachers.Count == 0)
            {
                ViewData["ErrorMessage"] = "No teachers found in the selected date range.";
            }

            return View("List", teachers);
        }
    }
}
