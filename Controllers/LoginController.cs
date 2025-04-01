using Microsoft.AspNetCore.Mvc;
using StudentManagement.Data;
    

namespace StudentManagement.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Login/Index
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Login/Index
        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Username and password must not be empty.";
                return View();
            }

            // Tìm user trong database (giả sử username là Name)
            var user = _context.Users.FirstOrDefault(u => u.Name == username);

            if (user != null && BCrypt.Verify(password, user.Password))
            {
                // Lưu thông tin vào Session
                HttpContext.Session.SetString("UserName", user.Name);
                HttpContext.Session.SetString("UserRole", user.Role);

                return RedirectToAction("Dashboard", "Register");
            }
            else
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }
        }
    }
}