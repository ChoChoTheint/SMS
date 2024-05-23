using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.DataModels;
using StudentManagementSystem.Models.ViewModels;

namespace StudentManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly SMSDbContext _dbContext;
        public CourseController(SMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Entry()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entry(CourseViewModel ui)
        {
            try
            {
                CourseEntity courseData = new CourseEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.UtcNow,
                    IsInActive = true,
                    Name = ui.Name,
                    Description = ui.Description,
                    OpeningDate = ui.OpeningDate,
                    DurationInHour = ui.DurationInHour,
                    DurationInMonth = ui.DurationInMonth,
                };
                _dbContext.Courses.Add(courseData);
                _dbContext.SaveChanges();
                TempData["info"] = "save successfully the record";
            }
            catch (Exception e)
            {
                TempData["info"] = "error while saving the record";
            }
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            IList<CourseViewModel> courseList = _dbContext.Courses.Select(s => new CourseViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                OpeningDate = s.OpeningDate,
                DurationInHour = s.DurationInHour,
                DurationInMonth = s.DurationInMonth,
            }).ToList();

            return View(courseList);
        }

        public IActionResult Delete(string Id)
        {
            try
            {
                var deleteCourseData = _dbContext.Courses.Where(w => w.Id == Id).FirstOrDefault();
                if(deleteCourseData is not null)
                {
                    _dbContext.Courses.Remove(deleteCourseData);
                    _dbContext.SaveChanges();
                }
                TempData["info"] = "delete successfully the record";
            }
            catch (Exception e)
            {
                TempData["info"] = "error while deleting the record";
            }
            return RedirectToAction("List");
        }

        public IActionResult Edit(string Id)
        {
            
                CourseViewModel editCourseData = _dbContext.Courses.Where(w => w.Id == Id).Select(s => new CourseViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    OpeningDate = s.OpeningDate,
                    DurationInHour = s.DurationInHour,
                    DurationInMonth = s.DurationInMonth,
                }).FirstOrDefault();

               
            return View(editCourseData);
        }

        [HttpPost]
        public IActionResult Update(CourseEntity ui)
        {
            try
            {
                CourseEntity updateCourseData = new CourseEntity()
                {
                    Id = ui.Id,
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                    IsInActive = true,
                    Description = ui.Description,
                    OpeningDate = ui.OpeningDate,
                    DurationInHour = ui.DurationInHour,
                    DurationInMonth = ui.DurationInMonth,
                };

                _dbContext.Courses.Update(updateCourseData);
                _dbContext.SaveChanges();
                TempData["info"] = "update successfully the record";
            }
            catch (Exception e)
            {
                TempData["info"] = "error while updating the record";
            }
            return RedirectToAction("List");
        }
    }
}
