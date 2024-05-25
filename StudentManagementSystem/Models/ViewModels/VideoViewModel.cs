namespace StudentManagementSystem.Models.ViewModels
{
    public class VideoViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile URL { get; set; }
        public string Video { get; set; }
        //[ForeignKey(nameof(CourseId))]
        public string CourseId { get; set; }
        public string CourseInfo { get; set; }
        //[ForeignKey(nameof(BatchId))]
        public string BatchId { get; set; }
        public string BatchInfo { get; set; }
    }
}
