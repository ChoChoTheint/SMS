using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models;
using StudentManagementSystem.Models.ViewModels;
using System.Diagnostics;

namespace StudentManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SMSDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, SMSDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        public IActionResult Index()
        {
            IList<AttendanceViewModel> attendanceList = (from attendance in _dbContext.Attendances
                                                         join student in _dbContext.Students
                                                         on attendance.StudentId equals student.Id
                                                         join batch in _dbContext.Batches
                                                         on student.BatchId equals batch.Id
                                                         select new AttendanceViewModel
                                                         {
                                                             Id = attendance.Id,
                                                             AttendanceDate = attendance.AttendanceDate,
                                                             InTime = attendance.InTime,
                                                             OutTime = attendance.OutTime,
                                                             IsLeave = attendance.IsLeave,
                                                             StudentId = student.Name + "/" + batch.Name,

                                                         }).ToList();


            return User.IsInRole("Admin")? View() : View(attendanceList);
        }
        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
