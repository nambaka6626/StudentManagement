namespace StudentManagement.Models
{
    public class StudentCourse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public User Student { get; set; }  // Chú ý: Student là User
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public float? Grade { get; set; } // Nếu bạn cần lưu điểm
    }
}
