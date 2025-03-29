using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentManagement.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            // Kiểm tra nếu người dùng không phải Admin, chuyển hướng về trang Login
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
    }
}
