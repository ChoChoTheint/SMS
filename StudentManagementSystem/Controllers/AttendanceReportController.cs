using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models.ViewModels;
using StudentManagementSystem.Reports;
using StudentManagementSystem.Utilities;

namespace StudentManagementSystem.Controllers
{
    public class AttendanceReportController : Controller
    {
        private readonly IAttendanceReport _attendanceReport;
        public AttendanceReportController(IAttendanceReport attendanceReport)
        {
            _attendanceReport = attendanceReport;
        }

        [Authorize]
        public IActionResult AttendanceDetailReport()
        {
            return View();
        }

        [Authorize]
        public IActionResult ReportByAttendanceFromCodeToCode(string fromCode, string toCode)
        {
            string reportName = $"AttendanceDetails_{Guid.NewGuid():N}.xlsx";

            IList<AttendanceReportViewModel> attendances = _attendanceReport.AttendanceDetailReport(fromCode, toCode);

            if (attendances.Any())
            {
                var exportData = FilesIOHelper.ExporttoExcel<AttendanceReportViewModel>(attendances, "attendanceDetailsReport");

                return File(exportData, "application/vnd.poenxmlformats-officedocument.spreedsheetml.sheet", reportName);
            }
            else
            {
                TempData["info"] = "There is no data when report to excel";
                return RedirectToAction("AttendanceDetailReport");
            }
        }
    }
}
