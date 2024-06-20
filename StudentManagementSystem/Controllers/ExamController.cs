using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.DataModels;
using StudentManagementSystem.Models.ViewModels;

namespace StudentManagementSystem.Controllers
{
    public class ExamController : Controller
    {
        private readonly SMSDbContext _dbContext;
        public ExamController(SMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        public IActionResult Entry()
        {
            ViewBag.Id = Guid.NewGuid().ToString();

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
        public IActionResult Entry(ExamViewModel ui)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    ExamEntity examData = new ExamEntity()
                    {
                        Id = ui.Id,
                        CreatedAt = DateTime.UtcNow,
                        IsInActive = true,
                        Name = ui.Name,
                        CourseId = ui.CourseId,
                        ExamDate = ui.ExamDate,
                    };
                    _dbContext.Exams.Add(examData);
                    _dbContext.SaveChanges();
                    TempData["info"] = "save successfully the record";
                }
                else
                {
                    //Reload id to populate the dropdown again
                    
                    ViewBag.Id = Guid.NewGuid().ToString();

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
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("list");
            }
            else
            {
                return Redirect("/Home/TeacherIndex.cshtml");
            }
        }

        [Authorize]
        public IActionResult List()
        {
            IList<ExamViewModel> examList = (from exam in _dbContext.Exams
                                             join course in _dbContext.Courses
                                             on exam.CourseId equals course.Id
             select new ExamViewModel
            {
                Id = exam.Id,
                Name = exam.Name,
                CourseId = course.Name,
                ExamDate = exam.ExamDate,
            }).ToList();

            return View(examList);
        }

        [Authorize]
        public IActionResult Detail()
        {
            IList<ExamViewModel> examDetail = _dbContext.Exams.Select(s => new ExamViewModel
                                            {
                                                Id = s.Id,
                                                Name = s.Name,
                                                ExamDate = s.ExamDate,
                                            }).ToList();
            return View(examDetail);
        }

        [Authorize]
        public IActionResult TeacherDetail(string Id)
        {
            IList<ExamViewModel> examDetail = (from exam in _dbContext.Exams
                                               join course in _dbContext.Courses
                                               on exam.CourseId equals course.Id
                                               join tc in _dbContext.TeacherCourses
                                               on course.Id equals tc.CourseId
                                               join teacher in _dbContext.Teachers
                                               on tc.TeacherId equals teacher.Id
                                               join batch in _dbContext.Batches
                                               on course.Id equals batch.CourseId

                                               where teacher.Email==Id

                                               select new ExamViewModel
            {
                Id = exam.Id,
                Name = exam.Name,
                CourseId = course.Name+"/ "+batch.Name,
                ExamDate = exam.ExamDate,
            }).ToList();
            return View(examDetail);
        }

        [Authorize]
        public IActionResult StudentDetail(string Id)
        {
            IList<ExamViewModel> examDetail = (from exam in _dbContext.Exams
                                               join course in _dbContext.Courses
                                               on exam.CourseId equals course.Id
                                               join batch in _dbContext.Batches
                                               on course.Id equals batch.CourseId
                                               join sb in _dbContext.StudentBatches
                                               on batch.Id equals sb.BatchId
                                               join student in _dbContext.Students
                                               on sb.StudentId equals student.Id

                                               where student.Email == Id

                                               select new ExamViewModel
                                               {
                                                   Id = exam.Id,
                                                   Name = exam.Name,
                                                   CourseId = course.Name + "/ " + batch.Name,
                                                   ExamDate = exam.ExamDate,
                                               }).ToList();
            return View(examDetail);
        }

        [Authorize]
        public IActionResult Delete(string Id)
        {
            try
            {
                var deleteExamData = _dbContext.Exams.Where(w => w.Id == Id).FirstOrDefault();
                if(deleteExamData is not null)
                {
                    _dbContext.Exams.Remove(deleteExamData);
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
            ExamViewModel editExamData = _dbContext.Exams.Where(w => w.Id == Id).Select(s => new ExamViewModel
            {
                Id = s.Id,
                Name = s.Name,
                CourseId = s.CourseId,
                ExamDate = s.ExamDate,
            }).FirstOrDefault();

            var courses = _dbContext.Courses.Select(s => new CourseViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Course = courses;

            return View(editExamData);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(ExamViewModel ui)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    ExamEntity updateExamData = new ExamEntity()
                    {
                        Id = ui.Id,
                        CreatedAt = DateTime.UtcNow,
                        IsInActive = true,
                        ModifiedAt = DateTime.UtcNow,
                        Name = ui.Name,
                        CourseId = ui.CourseId,
                        ExamDate = ui.ExamDate,
                    };
                    _dbContext.Exams.Update(updateExamData);
                    _dbContext.SaveChanges();
                    TempData["info"] = "update successfully the record";
                }
                else
                {
                    //Reload exam to populate the dropdown again.
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

        [Authorize]
        public IActionResult Cancle(ExamViewModel ui)
        {
            ExamEntity exam = new ExamEntity()
            {
                Name = "",
                ExamDate = DateTime.UtcNow,
            };
            return RedirectToAction("entry");
        }
    }
}
