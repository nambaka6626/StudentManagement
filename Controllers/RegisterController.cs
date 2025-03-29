using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using System.Collections.Generic;

namespace StudentManagement.Controllers
{
    public class UserController : Controller
    {
        private static List<User> users = new List<User>(); // Danh sách lưu tạm thời user

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
                Role = role // Gán role cho user
            });

            return RedirectToAction("Index", "Login"); // Chuyển về trang Login sau khi đăng ký
        }
    }
}
