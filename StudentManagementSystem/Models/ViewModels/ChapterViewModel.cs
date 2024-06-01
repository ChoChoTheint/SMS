using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class ChapterViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "batch is required")]
        public string BatchId { get; set; }
        [Required(ErrorMessage = "book is required")]
        public string BookId { get; set; }
        [Required(ErrorMessage = "video is required")]
        public string VideoId { get; set; }
    }
}
