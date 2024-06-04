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
        [Required(ErrorMessage = "in time is required")]
        [DataType(DataType.Time)]
        public DateTime InTime { get; set; }
        [Required(ErrorMessage = "out time is required")]
        [DataType(DataType.Time)]
        public DateTime OutTime { get; set; }
        [Required(ErrorMessage = "is leave is required")]
        public string IsLeave { get; set; }
        //[ForeignKey(nameof(AttendancePolicyId))]
        //public string AttendancePolicyId { get; set; }
        //[ForeignKey(nameof(StudentId))]
        [Required(ErrorMessage = "student is required")]
        public string StudentId { get; set; }
        //public string StudentInfo { get; set; }
    }
}
