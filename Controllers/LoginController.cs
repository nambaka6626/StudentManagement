using Microsoft.AspNetCore.Http; // Thêm thư viện này để dùng Session
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class LoginController : Controller
    {
        private static List<User> users = new List<User>
        {
            new User { Username = "admin", Password = "admin123", FullName = "Admin User", Email = "admin@example.com", PhoneNumber = "123456789", Role = "Admin" },
            new User { Username = "faculty", Password = "faculty123", FullName = "Faculty User", Email = "faculty@example.com", PhoneNumber = "987654321", Role = "Faculty" },
            new User { Username = "student", Password = "student123", FullName = "Student User", Email = "student@example.com", PhoneNumber = "456789123", Role = "Student" }
        };

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            var user = users.Find(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Role", user.Role);

                return RedirectToAction("Dashboard"); // Chuyển đến trang Dashboard sau khi đăng nhập
            }

            ViewBag.Error = "Invalid username or password";
            return View();
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        // ✅ Phương thức GET để hiển thị trang đăng ký
    [HttpGet]
    public IActionResult Register()
    {
        return View(); // Trả về trang Register.cshtml
    }

    // ✅ Phương thức POST để xử lý đăng ký tài khoản
    [HttpPost]
    public IActionResult Register(string username, string password, string fullName, string email, string phoneNumber, string role)
    {
        users.Add(new User 
        { 
            Username = username, 
            Password = password, 
            FullName = fullName,
            Email = email,
            PhoneNumber = phoneNumber,
            Role = role
        });

        return RedirectToAction("Index", "Login"); // Chuyển về trang đăng nhập
    }

    }
}
