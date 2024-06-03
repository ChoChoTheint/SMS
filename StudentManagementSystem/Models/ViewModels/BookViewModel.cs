using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class BookViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="name is required")]
        public string Name { get; set; }
        public string URL { get; set; } 
        [Required(ErrorMessage = "video is required")]
        public IFormFile File { get; set; }
        [Required(ErrorMessage = "description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "course is required")]
        public string CourseId { get; set; }
        [Required(ErrorMessage = "batch is required")]
        public string BatchId { get; set; }
    }
}
