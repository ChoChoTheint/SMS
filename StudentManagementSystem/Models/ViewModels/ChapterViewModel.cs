using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class ChapterViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //[ForeignKey(nameof(BatchId))]
        public string BatchId { get; set; }
        public string BatchInfo { get; set; }
        //[ForeignKey(nameof(BookId))]
        public string BookId { get; set; }
        public string BookInfo { get; set; }
        //[ForeignKey(nameof(VideoId))]
        public string VideoId { get; set; }
        public string VideoInfo { get; set; }
    }
}
