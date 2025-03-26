using Microsoft.AspNetCore.Mvc;
using StudentManagement.Data;
using StudentManagement.Repositories;

namespace StudentManagement.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _context;

        public StudentController(IUserRepository userRepository, ApplicationDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            var courses = _userRepository.GetStudentCourses(userId);
            var courseDetails = _context.Courses
                .Where(c => courses.Select(sc => sc.CourseId).Contains(c.CourseId))
                .Select(c => new { c.CourseName, Grade = courses.First(sc => sc.CourseId == c.CourseId).Grade })
                .ToList();
            return View(courseDetails);
        }
    }
}