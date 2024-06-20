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
        public string URL { get; set; }
        [Required(ErrorMessage = "Assignment is required")]
        public IFormFile File { get; set; }
        [Required(ErrorMessage = "Course is required")]
        public string CourseId { get; set; }
        [Required(ErrorMessage = "Batch is required")]
        public string BatchId { get; set; }
    }
}
