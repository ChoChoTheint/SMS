using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class BookViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        public string URL { get; set; } 
        [Required(ErrorMessage = "Book is required")]
        public IFormFile File { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Course is required")]
        public string CourseId { get; set; }
        [Required(ErrorMessage = "Batch is required")]
        public string BatchId { get; set; }
        
        public string BookURL { get; set; }
        
    }
}
