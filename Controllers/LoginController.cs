using Microsoft.AspNetCore.Mvc;

namespace StudentManagement.Controllers
{
    public class LoginController : Controller
    {
        // GET: /Login/Index
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Login/Index
        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            // Ở đây bạn cần xác thực username và password với dữ liệu từ cơ sở dữ liệu.
            // Ví dụ đơn giản: nếu username và password không rỗng, giả lập login thành công.
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Username and password must not be empty.";
                return View();
            }

            // Giả lập phân quyền dựa trên username (chỉ để demo)
            string role = "Student";
            if (username.ToLower() == "admin")
            {
                role = "Admin";
            }
            else if (username.ToLower() == "faculty")
            {
                role = "Faculty";
            }

            // Lưu thông tin đăng nhập vào TempData để chuyển hướng
            TempData["UserName"] = username;
            TempData["UserRole"] = role;

            // Chuyển hướng tới trang Dashboard của Login
            return RedirectToAction("Dashboard");
        }

        // Dashboard hiển thị sau khi đăng nhập thành công
        public IActionResult Dashboard()
        {
            ViewBag.UserName = TempData["UserName"]?.ToString();
            ViewBag.UserRole = TempData["UserRole"]?.ToString();
            return View();
        }
    }
}
