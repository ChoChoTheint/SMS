using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult Entry()
        {

            ViewBag.Id = Guid.NewGuid().ToString();

            var teachers = _dbContext.Teachers.Select(s => new TeacherViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Teacher = teachers;

            var courses = _dbContext.Courses.Select(s => new CourseViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Course = courses;

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Entry(TeacherCourseViewModel ui)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    TeacherCourseEntity teacherCourseData = new TeacherCourseEntity()
                    {
                        Id = ui.Id,
                        CreatedAt = DateTime.UtcNow,
                        IsInActive = true,
                        TeacherId = ui.TeacherId,
                        CourseId = ui.CourseId,
                    };
                    _dbContext.TeacherCourses.Add(teacherCourseData);
                    _dbContext.SaveChanges();
                    TempData["info"] = "save successfully the record";
                }
                else
                {
                    //Reload Id, TeacherId and CourseId to populate dropdown again

                    ViewBag.Id = Guid.NewGuid().ToString();

                    var teachers = _dbContext.Teachers.Select(s => new TeacherViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                    }).OrderBy(o => o.Name).ToList();
                    ViewBag.Teacher = teachers;

                    var courses = _dbContext.Courses.Select(s => new CourseViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                    }).OrderBy(o => o.Name).ToList();
                    ViewBag.Course = courses;

                    return View(ui);
                }
            }
            catch (Exception e)
            {
                TempData["info"] = "error while saving the record";
            }
            return RedirectToAction("List");
        }

        [Authorize]
        public IActionResult List()
        {
            IList<TeacherCourseViewModel> teacherCourseList = (from tc in _dbContext.TeacherCourses
                                                         join teacher in _dbContext.Teachers
                                                         on tc.TeacherId equals teacher.Id
                                                         join course in _dbContext.Courses
                                                         on tc.CourseId equals course.Id
                                                         

                                                         select new TeacherCourseViewModel
                                                         {
                                                             Id = tc.CourseId,
                                                             TeacherId = teacher.Name,
                                                             CourseId = course.Name,
                                                         }).ToList();
            return View(teacherCourseList);
        }

        [Authorize]
        public IActionResult Detail()
        {
            IList<TeacherCourseViewModel> teacherCourseList = (from tc in _dbContext.TeacherCourses
                                                               join teacher in _dbContext.Teachers
                                                               on tc.TeacherId equals teacher.Id
                                                               join course in _dbContext.Courses
                                                               on tc.CourseId equals course.Id


                                                               select new TeacherCourseViewModel
                                                               {
                                                                   Id = tc.CourseId,
                                                                   TeacherId = teacher.Name,
                                                                   CourseId = course.Name,
                                                               }).ToList();
            return View(teacherCourseList);
        }
        [Authorize]
        public IActionResult TeacherDetail()
        {
            IList<TeacherCourseViewModel> teacherDetail = (from tc in _dbContext.TeacherCourses
                                                           join teacher in _dbContext.Teachers
                                                           on tc.TeacherId equals teacher.Id
                                                           join course in _dbContext.Courses
                                                           on tc.CourseId equals course.Id
                                                           where tc.TeacherId == teacher.Id && tc.CourseId == course.Id

                                                           select new TeacherCourseViewModel
                                                           {
                                                               Id = tc.CourseId,
                                                               TeacherId = teacher.Name,
                                                               CourseId = course.Name,
                                                           }).ToList();
            return View(teacherDetail);
        }

        [Authorize]
        public IActionResult Delete(string Id)
        {
            try
            {
                var deleteTeacherCourseData = _dbContext.TeacherCourses.Where(w => w.Id == Id).FirstOrDefault();
                if(deleteTeacherCourseData is not null)
                {
                    _dbContext.TeacherCourses.Remove(deleteTeacherCourseData);
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

        [Authorize]
        public IActionResult Edit(string Id)
        {
            TeacherCourseViewModel editTeacherCourseData = _dbContext.TeacherCourses.Where(w => w.Id == Id).Select(s => new TeacherCourseViewModel
            {
                Id = s.Id,
                TeacherId = s.TeacherId,
                CourseId = s.CourseId,
                
            }).FirstOrDefault();

            var teachers = _dbContext.Teachers.Select(s => new TeacherViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Teacher = teachers;

            var courses = _dbContext.Courses.Select(s => new CourseViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Course = courses;

            return View(editTeacherCourseData);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(TeacherCourseViewModel ui)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    TeacherCourseEntity updateTeacherCourseData = new TeacherCourseEntity()
                    {
                        Id = ui.Id,
                        CreatedAt = DateTime.UtcNow,
                        ModifiedAt = DateTime.UtcNow,
                        IsInActive = true,
                        TeacherId = ui.TeacherId,
                        CourseId = ui.CourseId,
                    };
                    _dbContext.TeacherCourses.Update(updateTeacherCourseData);
                    _dbContext.SaveChanges();
                    TempData["info"] = "update successfully the record";
                }
                else
                {
                    var teachers = _dbContext.Teachers.Select(s => new TeacherViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                    }).OrderBy(o => o.Name).ToList();
                    ViewBag.Teacher = teachers;

                    var courses = _dbContext.Courses.Select(s => new CourseViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                    }).OrderBy(o => o.Name).ToList();
                    ViewBag.Course = courses;

                    return View("Edit", model: ui);
                }
            }
            catch (Exception e)
            {
                TempData["info"] = "error while updating the record";
            }
            return RedirectToAction("List");
        }

    }
}
