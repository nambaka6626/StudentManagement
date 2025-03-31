using System;

namespace StudentManagement.Models
{
    public class FacultyUser : IUser
    {
        public string Name { get; set; }
        public string Role => "Faculty";

        public FacultyUser(string name)
        {
            Name = name;
        }

        public void Register()
        {
            // Giả lập logic đăng ký cho Faculty
            Console.WriteLine($"Faculty '{Name}' đã được đăng ký.");
        }
    }
}
