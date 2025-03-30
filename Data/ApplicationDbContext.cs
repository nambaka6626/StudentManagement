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
    modelBuilder.Entity<Course>().HasKey(c => c.CourseId);
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

    // ✅ Thêm khóa học mặc định
    modelBuilder.Entity<Course>().HasData(
        new Course { CourseId = 1, Name = "Math 101", Description = "Basic Mathematics" },
        new Course { CourseId = 2, Name = "Physics 101", Description = "Introduction to Physics" }
    );
}

    }
}