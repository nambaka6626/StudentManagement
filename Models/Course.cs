using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        public string CourseName { get; set; } // Đảm bảo rằng tên khóa học không null hoặc rỗng

        public string Description { get; set; }
    }
}
