using System;

namespace StudentManagement.Models
{
    public class StudentUser : IUser
    {
        public string Name { get; set; }
        public string Role => "Student";

        public StudentUser(string name)
        {
            Name = name;
        }

        public void Register()
        {
            // Giả lập logic đăng ký cho Student
            Console.WriteLine($"Student '{Name}' đã được đăng ký.");
        }
    }
}
