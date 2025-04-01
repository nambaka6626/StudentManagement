using Microsoft.AspNetCore.Mvc;
using StudentManagement.Data;
using StudentManagement.Models;
using System.Linq;

namespace StudentManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách khóa học
        public IActionResult ManageCourses()
        {
            var courses = _context.Courses.ToList();
            return View(courses);
        }

        // Trang tạo khóa học mới
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
                return RedirectToAction("ManageCourses");
            }
            return View(course);
        }

        // Trang sửa khóa học
        public IActionResult Edit(int id)
        {
            var course = _context.Courses.Find(id);
            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Update(course);
                _context.SaveChanges();
                return RedirectToAction("ManageCourses");
            }
            return View(course);
        }

        // Xóa khóa học
        public IActionResult Delete(int id)
        {
            var course = _context.Courses.Find(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }
            return RedirectToAction("ManageCourses");
        }
    }
}
