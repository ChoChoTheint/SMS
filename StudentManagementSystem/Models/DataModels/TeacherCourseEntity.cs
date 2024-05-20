using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.DataModels
{
    [Table("TeacherCourse")]
    public class TeacherCourseEntity : BaseEntity
    {
        [ForeignKey(nameof(TeacherId))]
        public string TeacherId { get; set; }

        [ForeignKey(nameof(CourseId))]
        public string CourseId { get; set; }
    }
}
