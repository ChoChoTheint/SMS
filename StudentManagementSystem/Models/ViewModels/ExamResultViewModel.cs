using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class ExamResultViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="mark is required")]
        public int Mark { get; set; }
        [Required(ErrorMessage = "student is required")]
        public string StudentId { get; set; }
    }
}
