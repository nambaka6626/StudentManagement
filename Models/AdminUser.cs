using System;

namespace StudentManagement.Models
{
    public class AdminUser : IUser
    {
        public string Name { get; set; }
        public string Role => "Admin";

        public AdminUser(string name)
        {
            Name = name;
        }

        public void Register()
        {
            // Giả lập logic đăng ký cho Admin
            Console.WriteLine($"Admin '{Name}' đã được đăng ký.");
        }
    }
}
