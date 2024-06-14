using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.ViewModels;
using System;

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
                                                            join sb in _dbContext.StudentBatches
                                                            on attendance.StudentId equals sb.StudentId
                                                           join student in _dbContext.Students
                                                           on attendance.StudentId equals student.Id
                                                           join batch in _dbContext.Batches
                                                           on sb.BatchId equals batch.Id

                                                           where attendance.Id.CompareTo(fromCode) >= 0 && attendance.Id.CompareTo(toCode) >= 0
                                                           select new AttendanceReportViewModel
                                                           {
                                                               StudentInfo = student.Name+"/ "+batch.Name,
                                                               AttendanceDate = attendance.AttendanceDate.ToString("dd-MM-yyyy"),
                                                               InTime = attendance.InTime.ToString("HH-mm-ss"),
                                                               OutTime = attendance.OutTime.ToString("HH-mm-ss"),
                                                               IsLeave = attendance.IsLeave,
                                                           }).ToList();

            return attendances;
        }
    }
}
