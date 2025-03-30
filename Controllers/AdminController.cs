using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Data; // Import DbContext
using StudentManagement.Models; // Import Model
using System.Linq;

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
            // Kiểm tra nếu người dùng không phải Admin, chuyển hướng về trang Login
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Index", "Login");
            }
            Console.WriteLine($"Session Role: {HttpContext.Session.GetString("Role")}");


            // Lấy danh sách khóa học từ database
            var courses = _context.Courses?.ToList() ?? new List<Course>();


            // ✅ Debug kiểm tra danh sách khóa học
            Console.WriteLine($"Total courses: {courses.Count}");

            // ✅ Kiểm tra số lượng khóa học trong database
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
    if (ModelState.IsValid)
    {
        _context.Courses.Add(course);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
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

    }
}
