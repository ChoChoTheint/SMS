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
            IList<AdminViewModel> adminList = _dbContext.Admins.Select(s => new AdminViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Phone = s.Phone,
                Address = s.Address,
                DOB = s.DOB,
                NRC = s.NRC,
                FatherName = s.FatherName,
                Gender = s.Gender,
            }).ToList();

            ViewBag.TeacherCount = _dbContext.TeacherCourses.Count();
            ViewBag.StudentCount = _dbContext.Students.Count();
            ViewBag.CourseCount = _dbContext.Courses.Count();
            ViewBag.BatchCount = _dbContext.Batches.Count();
            ViewBag.AttendanceCount = _dbContext.Attendances.Count();
            ViewBag.AssignmentCount = _dbContext.Assignments.Count();
            ViewBag.BookCount = _dbContext.Books.Count();
            ViewBag.ExamCount = _dbContext.Exams.Count();
            ViewBag.ExamResultCount = _dbContext.ExamResults.Count();

            return User.IsInRole("Admin")?  View("Index", model: adminList) :  View();
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
