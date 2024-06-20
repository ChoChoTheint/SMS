using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.DataModels
{
    [Table("Exam")]
    public class ExamEntity : BaseEntity
    {
        public string Name { get; set; }
        [ForeignKey(nameof(CourseId))]
        public string CourseId { get; set; }
        public DateTime ExamDate { get; set; }
    }
}
