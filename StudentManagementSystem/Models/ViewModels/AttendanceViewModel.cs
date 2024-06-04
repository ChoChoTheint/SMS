using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class AttendanceViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="attendance date is required")]
        [DataType(DataType.Date)]
        public DateTime AttendanceDate { get; set; }
        [Required(ErrorMessage = "intime is required")]
        [DataType(DataType.Time)]
        public DateTime InTime { get; set; }
        [Required(ErrorMessage = "outtime is required")]
        [DataType(DataType.Time)]
        public DateTime OutTime { get; set; }
        [Required(ErrorMessage = "isleave is required")]
        public string IsLeave { get; set; }
        [Required(ErrorMessage = "student is required")]
        public string StudentId { get; set; }
    }
}
