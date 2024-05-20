using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.DataModels
{
    [Table("ExamResult")]
    public class ExamResultEntity : BaseEntity
    {
        public int Mark { get; set; }
        [ForeignKey(nameof(StudentId))]
        public string StudentId { get; set; }
    }
}
