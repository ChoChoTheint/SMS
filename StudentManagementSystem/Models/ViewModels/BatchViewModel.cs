using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class BatchViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Course is required")]
        public string CourseId { get; set; }
        [Required(ErrorMessage = "opening data is required")]
        [DataType(DataType.Date)]
        public DateTime OpeningDate { get; set; }
        [Required(ErrorMessage = "duration in hour is required")]
        public string DurationInHour { get; set; }
        [Required(ErrorMessage = "duration in month is required")]
        public string DurationInMonth { get; set; }
    }
}
