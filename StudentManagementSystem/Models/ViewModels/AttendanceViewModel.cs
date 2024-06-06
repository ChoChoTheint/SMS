using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class AttendanceViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Attendance date is required")]
        [DataType(DataType.Date)]
        public DateTime AttendanceDate { get; set; }
        [Required(ErrorMessage = "In time is required")]
        [DataType(DataType.Time)]
        public DateTime InTime { get; set; }
        [Required(ErrorMessage = "Out time is required")]
        [DataType(DataType.Time)]
        public DateTime OutTime { get; set; }
        [Required(ErrorMessage = "Leave is required")]
        public string IsLeave { get; set; }
        [Required(ErrorMessage = "Student Name is required")]
        public string StudentId { get; set; }
    }
}
