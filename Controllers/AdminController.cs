using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Data; // Import DbContext
using StudentManagement.Models; // Import Model
using System.Linq;
using StudentManagement.Models.ViewModels;
using Microsoft.EntityFrameworkCore;



namespace StudentManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
{
    if (HttpContext.Session.GetString("Role") != "Admin")
    {
        return RedirectToAction("Index", "Login");
    }

    // Lấy danh sách khóa học từ database
    var courses = _context.Courses?.ToList() ?? new List<Course>();

    // Kiểm tra danh sách khóa học đã lấy đúng chưa
    Console.WriteLine($"Total courses: {courses.Count}");

    ViewBag.TotalCourses = courses.Count;

    return View(courses); // Truyền danh sách khóa học vào View
}


        // Thêm chức năng tạo khóa học
        public IActionResult Create()
{
    return View();
}

[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(Course course)
{
    Console.WriteLine($"Debug: CourseName = {course.CourseName}, Description = {course.Description}");
    
    if (ModelState.IsValid)
    {
        _context.Courses.Add(course);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    Console.WriteLine("ModelState is not valid!");
    return View(course);
}



        // Thêm chức năng sửa khóa học
        public IActionResult Edit(int id)
{
    var course = _context.Courses.Find(id);
    if (course == null)
    {
        return NotFound();
    }
    return View(course);
}


[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Edit(Course course)
{
    if (ModelState.IsValid)
    {
        _context.Courses.Update(course);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    return View(course);
}



        // Thêm chức năng xóa khóa học
        public IActionResult Delete(int id)
{
    var course = _context.Courses.Find(id);
    if (course != null)
    {
        _context.Courses.Remove(course);
        _context.SaveChanges();
    }
    return RedirectToAction(nameof(Index));
}

public IActionResult AssignedCourses()
{
    var assignedCourses = _context.StudentCourses
        .Include(sc => sc.Student)
        .Include(sc => sc.Course)
        .ToList();

    return View(assignedCourses);  
}




//xu ly admin gan khoa học cho sinh viênviên
[HttpPost]
public IActionResult AssignedCourses(int studentId, int courseId)
{
    // Kiểm tra xem sinh viên đã được gán khóa học chưa
    var existingAssignment = _context.StudentCourses
        .FirstOrDefault(sc => sc.StudentId == studentId && sc.CourseId == courseId);

    if (existingAssignment == null) // Nếu chưa có, thêm mới
    {
        var studentCourse = new StudentCourse
        {
            StudentId = studentId,
            CourseId = courseId
        };

        _context.StudentCourses.Add(studentCourse);
        _context.SaveChanges();
    }

    return RedirectToAction("Index");
}


    }
}
