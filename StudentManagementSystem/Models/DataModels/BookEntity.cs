using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.DataModels
{
    [Table("Book")]
    public class BookEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        [ForeignKey(nameof(CourseId))]
        public string CourseId { get; set; }
        [ForeignKey(nameof(BatchId))]
        public string BatchId { get; set; }
    }
}
