using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.DataModels;
using StudentManagementSystem.Models.ViewModels;

namespace StudentManagementSystem.Controllers
{
    public class TeacherCourseController : Controller
    {
        private readonly SMSDbContext _dbContext;
        public TeacherCourseController(SMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Entry()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entry(TeacherCourseViewModel ui)
        {
            try
            {
                TeacherCourseEntity teacherCourseData = new TeacherCourseEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.UtcNow,
                    IsInActive = true,
                    TeacherId = ui.TeacherId,
                    CourseId = ui.CourseId,
                };
                _dbContext.TeacherCourses.Add(teacherCourseData);
                _dbContext.SaveChanges();
                TempData["info"] = "save successfully the record";
            }
            catch (Exception e)
            {
                TempData["info"] = "error while saving the record";
            }
            return RedirectToAction("List");
        }
    }
}
