using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class BookViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //[ForeignKey(nameof(CourseId))]
        public string CourseId { get; set; }
        public string CourseInfo { get; set; }
        //[ForeignKey(nameof(BatchId))]
        public string BatchId { get; set; }
        public string BatchInfo { get; set; }
    }
}
