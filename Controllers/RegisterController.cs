using Microsoft.AspNetCore.Mvc;
using StudentManagement.Factories;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
	public class RegisterController : Controller
	{
		// GET: /Register/Index
		public IActionResult Index()
		{
			return View();
		}

		// POST: /Register/Register
		[HttpPost]
		public IActionResult Register(string name, string role)
		{
			try
			{
				IUser user = UserFactory.CreateUser(role, name);
				user.Register();
				TempData["UserName"] = user.Name;
				TempData["UserRole"] = user.Role;
				return RedirectToAction("Dashboard");
			}
			catch (Exception ex)
			{
				ViewBag.Error = ex.Message;
				return View("Index");
			}
		}



		public IActionResult Dashboard()
		{
			// Lấy thông tin user từ TempData và hiển thị giao diện tương ứng
			ViewBag.UserName = TempData["UserName"]?.ToString();
			ViewBag.UserRole = TempData["UserRole"]?.ToString();
			return View();
		}
	}
}
