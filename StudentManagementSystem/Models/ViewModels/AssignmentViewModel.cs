using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class AssignmentViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public string URL { get; set; } = null;
        [Required(ErrorMessage = "File is required")]
        public IFormFile File { get; set; }
        //[ForeignKey(nameof(CourseId))]
        [Required(ErrorMessage = "Course is required")]
        public string CourseId { get; set; }
        public string CourseInfo { get; set; }
        //[ForeignKey(nameof(BatchId))]
        [Required(ErrorMessage = "Batch is required")]
        public string BatchId { get; set; }
        public string BatchInfo { get; set; }
    }
}
