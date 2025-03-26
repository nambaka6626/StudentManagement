using StudentManagement.Models;

namespace StudentManagement.Repositories
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
        void AddUser(User user);
        List<StudentCourse> GetStudentCourses(int studentId);
    }
}