using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models.ViewModels
{
    public class CourseViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Opening data is required")]
        [DataType(DataType.Date)]
        public DateTime OpeningDate { get; set; }
        [Required(ErrorMessage = "Duration in hour is required")]
        public string DurationInHour { get; set; }
        [Required(ErrorMessage = "Duration in month is required")]
        public string DurationInMonth { get; set; }
    }
}
