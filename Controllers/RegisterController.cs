using Microsoft.AspNetCore.Mvc;
using StudentManagement.Factories;
using StudentManagement.Models;
using StudentManagement.Data;
using BCrypt.Net;

namespace StudentManagement.Controllers
{
    public class RegisterController : Controller
    {
        private readonly AppDbContext _context;

        public RegisterController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Register/Index
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Register/Register
        [HttpPost]
        public IActionResult Register(string name, string email, string phone, string password, string role)
        {
            try
            {
                IUser user = UserFactory.CreateUser(role, name);

                var newUser = new User
                {
                    Name = name,
                    Email = email,
                    Phone = phone,
                    Password = BCrypt.HashPassword(password), // Mã hóa mật khẩu
                    Role = role
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Index");
            }
        }

        public IActionResult Dashboard()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(ViewBag.UserName))
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        // Action để xử lý lỗi (dùng cho UseExceptionHandler trong Program.cs)
        public IActionResult Error()
        {
            return View();
        }
    }
}