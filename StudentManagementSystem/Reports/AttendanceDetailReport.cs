using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.ViewModels;

namespace StudentManagementSystem.Reports
{
    public class AttendanceDetailReport : IAttendanceReport
    {
        private readonly SMSDbContext _dbContext;
        public AttendanceDetailReport(SMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        IList<AttendanceReportViewModel> IAttendanceReport.AttendanceDetailReport(string fromCode, string toCode)
        {
            IList<AttendanceReportViewModel> attendances = (from attendance in _dbContext.Attendances
                                                           join student in _dbContext.Students
                                                           on attendance.StudentId equals student.Id

                                                           where attendance.Id.CompareTo(fromCode) >= 0 && attendance.Id.CompareTo(toCode) <= 0
                                                           select new AttendanceReportViewModel
                                                           {
                                                               AttendanceDate = attendance.AttendanceDate,
                                                               InTime = attendance.InTime,
                                                               OutTime = attendance.OutTime,
                                                               IsLeave = attendance.IsLeave,
                                                               StudentInfo = student.Name,
                                                           }).ToList();

            return attendances;
        }
    }
}
