using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.DataModels
{
    [Table("Chapter")]
    public class ChapterEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey(nameof(BatchId))]
        public string BatchId { get; set; }
        [ForeignKey(nameof(BookId))]
        public string BookId { get; set; }
        [ForeignKey(nameof(VideoId))]
        public string VideoId { get; set; }
    }
}
