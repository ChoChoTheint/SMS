using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class AttendanceReportViewModel
    {
        public string StudentInfo { get; set; }
        public string AttendanceDate { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string IsLeave { get; set; }
        
    }
}
