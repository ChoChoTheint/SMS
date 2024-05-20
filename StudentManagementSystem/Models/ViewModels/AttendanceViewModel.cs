using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class AttendanceViewModel
    {
        public string Id { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public bool IsLeave { get; set; }
        [ForeignKey(nameof(AttendancePolicyId))]
        public string AttendancePolicyId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public string StudentId { get; set; }
        public string StudentInfo { get; set; }
    }
}
