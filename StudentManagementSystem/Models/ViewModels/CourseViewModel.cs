using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models.ViewModels
{
    public class CourseViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "opening data is required")]
        [DataType(DataType.Date)]
        public DateTime OpeningDate { get; set; }
        [Required(ErrorMessage = "duration in hour is required")]
        public string DurationInHour { get; set; }
        [Required(ErrorMessage = "duration in month is required")]
        public string DurationInMonth { get; set; }
    }
}
