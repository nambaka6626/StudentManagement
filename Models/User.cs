namespace StudentManagement.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;  // Kiểm tra có thuộc tính này chưa
        public string PhoneNumber { get; set; } = string.Empty; // Kiểm tra có thuộc tính này chưa
        public string Role { get; set; } = "Student";  // Kiểm tra có thuộc tính này chưa
    }
}



