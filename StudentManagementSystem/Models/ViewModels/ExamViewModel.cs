using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class ExamViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Course is required")]
        public string CourseId { get; set; }
        [Required(ErrorMessage = "Exam date is required")]
        [DataType(DataType.Date)]
        public DateTime ExamDate { get; set; }
    }
}
