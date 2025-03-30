using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;

namespace StudentManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<User>().HasKey(u => u.Id);
    modelBuilder.Entity<Course>().HasKey(c => c.Id); // Sửa từ CourseId -> Id


    modelBuilder.Entity<StudentCourse>().HasKey(sc => sc.Id);

    modelBuilder.Entity<StudentCourse>()
        .HasOne(sc => sc.Student)
        .WithMany()
        .HasForeignKey(sc => sc.StudentId);

    modelBuilder.Entity<StudentCourse>()
        .HasOne(sc => sc.Course)
        .WithMany()
        .HasForeignKey(sc => sc.CourseId);

    // ✅ Thêm admin mặc định vào database
    modelBuilder.Entity<User>().HasData(
        new User
        {
            Id = 1,
            Username = "admin",
            Password = "admin123",
            FullName = "Admin User",
            Email = "admin@example.com",
            PhoneNumber = "123456789",
            Role = "Admin"
        }
    );

    modelBuilder.Entity<Course>().HasData(
    new Course { Id = 1, CourseName = "Math", Description = "Basic Math" },
    new Course { Id = 2, CourseName = "Physics", Description = "Intro to Physics" }
);




}

    }
}