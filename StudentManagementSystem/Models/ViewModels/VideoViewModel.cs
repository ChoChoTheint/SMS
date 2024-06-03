using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models.ViewModels
{
    public class VideoViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="name is required*")]
        public string Name { get; set; }
        [Required(ErrorMessage = "description is required*")]
        public string Description { get; set; }
        public string URL { get; set; }
        [Required(ErrorMessage = "Video is required*")]
        public IFormFile VideoFile { get; set; }
        [Required(ErrorMessage = "course is required*")]
        public string CourseId { get; set; }
        [Required(ErrorMessage = "batch is required*")]
        public string BatchId { get; set; }
    }
}
