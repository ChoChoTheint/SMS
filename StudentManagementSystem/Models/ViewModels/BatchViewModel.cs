using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class BatchViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "course is required")]
        public string CourseId { get; set; }
        //public string CourseInfo { get; set; }
    }
}
