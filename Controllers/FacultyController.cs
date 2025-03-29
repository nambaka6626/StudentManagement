using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentManagement.Controllers
{
    public class FacultyController : Controller
    {
        public IActionResult Index()
        {
            // Kiểm tra nếu người dùng không phải Faculty, chuyển hướng về trang Login
            if (HttpContext.Session.GetString("Role") != "Faculty")
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
    }
}
