using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
    public class Course
    {
        public int CourseId { get; set; }  // Khóa chính
        public string Name { get; set; }  = string.Empty;   // ✅ Đảm bảo có Name
        public string Description { get; set; } = string.Empty;  // ✅ Đảm bảo có Description
    }
}

