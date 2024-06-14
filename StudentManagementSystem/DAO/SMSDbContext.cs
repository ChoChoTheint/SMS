using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models.DataModels;

namespace StudentManagementSystem.DAO
{
    public class SMSDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public SMSDbContext(DbContextOptions<SMSDbContext> options) : base(options) { }
        public DbSet<AdminEntity> Admins { get; set; }
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<StudentBatchEntity> StudentBatches { get; set; }
        public DbSet<TeacherEntity> Teachers { get; set; }
        public DbSet<AttendanceEntity> Attendances { get; set; }
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<ChapterEntity> Chapters { get; set; }
        public DbSet<BatchEntity> Batches { get; set; }
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<VideoEntity> Videos { get; set; }
        public DbSet<AssignmentEntity> Assignments { get; set; }
        public DbSet<TeacherCourseEntity> TeacherCourses { get; set; }
        public DbSet<ExamEntity> Exams { get; set; }
        public DbSet<ExamResultEntity> ExamResults { get; set; }
    }
}
