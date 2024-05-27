using StudentManagementSystem.Models.ViewModels;

namespace StudentManagementSystem.Reports
{
    public interface IAttendanceReport
    {
        IList<AttendanceReportViewModel> AttendanceDetailReport(string fromCode, string toCode);
    }
}
