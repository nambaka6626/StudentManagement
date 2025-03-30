using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Data;
using StudentManagement.Models;
using System.Linq;
using System;

namespace StudentManagement.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            // 🔍 Debug: In danh sách User trong database
            var allUsers = _context.Users.ToList();
            Console.WriteLine("🔍 Danh sách User trong database:");
            foreach (var u in allUsers)
            {
                Console.WriteLine($"User: {u.Username}, Role: {u.Role}");
            }

            // 🛠 Kiểm tra đăng nhập
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                Console.WriteLine($"✅ User found: {user.Username}, Role: {user.Role}");

                // 🛑 BƯỚC 3: Reset Session trước khi đăng nhập
                HttpContext.Session.Clear();

                // ✅ Lưu thông tin đăng nhập vào session
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Role", user.Role);

                // 🔥 Debug: Kiểm tra session sau khi set
                Console.WriteLine($"🚀 Session Set: {HttpContext.Session.GetString("Username")} - {HttpContext.Session.GetString("Role")}");

                return RedirectToAction("Dashboard");
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

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string password, string fullName, string email, string phoneNumber, string role)
        {
            var newUser = new User
            {
                Username = username,
                Password = password,
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                Role = role
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            // 🛠 Chỉ cho phép đăng nhập sau khi đăng ký
            return RedirectToAction("Index", "Login");
        }
    }
}
