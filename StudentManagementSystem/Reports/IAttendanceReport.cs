using StudentManagementSystem.Models.ViewModels;
using System;

namespace StudentManagementSystem.Reports
{
    public interface IAttendanceReport
    {
        IList<AttendanceReportViewModel> AttendanceDetailReport(string fromCode, string toCode);
    }
}
