using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.DataModels
{
    [Table("StudentBatch")]
    public class StudentBatchEntity:BaseEntity
    {
        [ForeignKey(nameof(StudentId))]
        public string StudentId { get; set; }
        [ForeignKey(nameof(BatchId))]
        public string BatchId { get; set; }
    }
}
