using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class ChapterViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Batch is required")]
        public string BatchId { get; set; }
        [Required(ErrorMessage = "Book is required")]
        public string BookId { get; set; }
        [Required(ErrorMessage = "Video is required")]
        public string VideoId { get; set; }
    }
}
