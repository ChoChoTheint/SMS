using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class TeacherCourseViewModel
    {
        public string Id { get; set; }
        //[ForeignKey(nameof(TeacherId))]
        public string TeacherId { get; set; }
        public string TeacherInfo { get; set; }

        //[ForeignKey(nameof(CourseId))]
        public string CourseId { get; set; }
        public string CourseInfo { get; set; }

    }
}
