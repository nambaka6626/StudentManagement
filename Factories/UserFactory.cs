using System;
using StudentManagement.Models;

namespace StudentManagement.Factories
{
    public static class UserFactory
    {
        public static IUser CreateUser(string role, string name)
        {
            return role switch
            {
                "Admin" => new AdminUser(name),
                "Faculty" => new FacultyUser(name),
                "Student" => new StudentUser(name),
                _ => throw new ArgumentException("Role không hợp lệ", nameof(role))
            };
        }
    }
}
