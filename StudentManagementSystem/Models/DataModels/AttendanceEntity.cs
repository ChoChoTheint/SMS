using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.DataModels
{
    [Table("Attendance")]
    public class AttendanceEntity : BaseEntity
    {
        public DateTime AttendanceDate { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public bool IsLeave { get; set; }
        [ForeignKey(nameof(StudentId))]
        public string StudentId { get; set; }
    }
}
