using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.DataModels;
using StudentManagementSystem.Models.ViewModels;

namespace StudentManagementSystem.Controllers
{
    public class StudentBatchController : Controller
    {
        private readonly SMSDbContext _dbContext;
        public StudentBatchController(SMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [Authorize]
        public IActionResult Index()
        {
            ViewBag.Id = Guid.NewGuid().ToString();

            ViewBag.Student = _dbContext.Students.Select(s => new StudentViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();

            var batches = (from batch in _dbContext.Batches
                           join course in _dbContext.Courses
                           on batch.CourseId equals course.Id

                           select new BatchViewModel
                           {
                               Id = batch.Id,
                               Name = batch.Name + "/ " + course.Name
                           }).OrderBy(o => o.Name).ToList();
            ViewBag.Batch = batches;

            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Index(StudentBatchViewModel ui)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    StudentBatchEntity studentData = new StudentBatchEntity()
                    {
                        Id = ui.Id,
                        CreatedAt = DateTime.UtcNow,
                        IsInActive = true,
                        StudentId = ui.StudentId,
                        BatchId = ui.BatchId,
                    };
                    _dbContext.StudentBatches.Add(studentData);
                    _dbContext.SaveChanges();
                    TempData["info"] = "save successfully the data";
                }
                else
                {
                    ViewBag.Id = Guid.NewGuid().ToString();

                    ViewBag.Student = _dbContext.Students.Select(s => new StudentViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                    }).OrderBy(o => o.Name).ToList();

                    var batches = (from batch in _dbContext.Batches
                                   join course in _dbContext.Courses
                                   on batch.CourseId equals course.Id

                                   select new BatchViewModel
                                   {
                                       Id = batch.Id,
                                       Name = batch.Name + "/ " + course.Name
                                   }).OrderBy(o => o.Name).ToList();
                    ViewBag.Batch = batches;

                    return View(ui);
                }
            }
            catch (Exception e)
            {
                TempData["info"] = "error while saving the data";
            }
            return RedirectToAction("List");
        }

        [Authorize]
        public IActionResult List()
        {
            IList<StudentBatchViewModel> studentList = (from sb in _dbContext.StudentBatches
                                                        join student in _dbContext.Students
                                                        on sb.StudentId equals student.Id
                                                        join batch in _dbContext.Batches
                                                        on sb.BatchId equals batch.Id
                                                        join course in _dbContext.Courses
                                                        on batch.CourseId equals course.Id

                                                        select new StudentBatchViewModel
                                                        {
                                                            Id = sb.Id,
                                                            StudentId = student.Name,
                                                            BatchId = batch.Name+"/ "+course.Name,
                                                        }).ToList();
            return View(studentList);
        }

        [Authorize]
        public IActionResult Detail()
        {
            IList<StudentBatchViewModel> studentDetail = (from sb in _dbContext.StudentBatches
                                                        join student in _dbContext.Students
                                                        on sb.StudentId equals student.Id
                                                        join batch in _dbContext.Batches
                                                        on sb.BatchId equals batch.Id
                                                        join course in _dbContext.Courses
                                                        on batch.CourseId equals course.Id

                                                        select new StudentBatchViewModel
                                                        {
                                                            Id = sb.BatchId,
                                                            StudentId = student.Name,
                                                            BatchId = batch.Name + "/ " + course.Name,
                                                        }).ToList();
            return View(studentDetail);
        }
        [Authorize]
        public IActionResult StudentDetail()
        {
            IList<StudentBatchViewModel> studentBatchDetail = (from sb in _dbContext.StudentBatches
                                                               join student in _dbContext.Students
                                                               on sb.StudentId equals student.Id
                                                               join batch in _dbContext.Batches
                                                               on sb.BatchId equals batch.Id
                                                               where sb.StudentId == student.Id && sb.BatchId == batch.Id

                                                               select new StudentBatchViewModel
                                                               {
                                                                   Id = sb.BatchId,
                                                                   StudentId = student.Name,
                                                                   BatchId = batch.Name,
                                                               }).ToList();
            return View(studentBatchDetail);
        }

        [Authorize]
        public IActionResult Delete(string Id)
        {
            try
            {
                var deleteStudentBatch = _dbContext.StudentBatches.Where(w => w.Id == Id).FirstOrDefault();
                if(deleteStudentBatch is not null)
                {
                    _dbContext.StudentBatches.Remove(deleteStudentBatch);
                    _dbContext.SaveChanges();
                }
                TempData["info"] = "delete successtully the data";
            }
            catch (Exception e)
            {
                TempData["info"] = "error while deleting the data";
            }
            return RedirectToAction("List");
        }

        [Authorize]
        public IActionResult Edit(string Id)
        {
            StudentBatchViewModel editStudentBatchData = _dbContext.StudentBatches.Where(w => w.Id == Id).Select(s => new StudentBatchViewModel
            {
                Id =s.Id,
                StudentId=s.StudentId,
                BatchId=s.BatchId,
            }).FirstOrDefault();

            ViewBag.Student = _dbContext.Students.Select(s => new StudentViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();

            var batches = (from batch in _dbContext.Batches
                           join course in _dbContext.Courses
                           on batch.CourseId equals course.Id

                           select new BatchViewModel
                           {
                               Id = batch.Id,
                               Name = batch.Name + "/ " + course.Name
                           }).OrderBy(o => o.Name).ToList();
            ViewBag.Batch = batches;

            return View(editStudentBatchData);
        }

        [Authorize]
        public IActionResult Update(StudentBatchViewModel ui)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    StudentBatchEntity studentData = new StudentBatchEntity()
                    {
                        Id = ui.Id,
                        CreatedAt = DateTime.UtcNow,
                        ModifiedAt = DateTime.UtcNow,
                        IsInActive = true,
                        StudentId = ui.StudentId,
                        BatchId = ui.BatchId,
                    };
                    _dbContext.StudentBatches.Update(studentData);
                    _dbContext.SaveChanges();
                    TempData["info"] = "update successfully the data";
                }
                else
                {
                    ViewBag.Id = Guid.NewGuid().ToString();

                    ViewBag.Student = _dbContext.Students.Select(s => new StudentViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                    }).OrderBy(o => o.Name).ToList();

                    var batches = (from batch in _dbContext.Batches
                                   join course in _dbContext.Courses
                                   on batch.CourseId equals course.Id

                                   select new BatchViewModel
                                   {
                                       Id = batch.Id,
                                       Name = batch.Name + "/ " + course.Name
                                   }).OrderBy(o => o.Name).ToList();
                    ViewBag.Batch = batches;

                    return View(ui);
                }
            }
            catch (Exception e)
            {
                TempData["info"] = "error while updating the data";
            }
            return RedirectToAction("List");
        }

        [Authorize]
        public IActionResult Cancle(StudentBatchViewModel ui)
        {
            StudentBatchViewModel studentBatch = new StudentBatchViewModel()
            {
                StudentId = "",
                BatchId = ""
            };
            return RedirectToAction("index");
        }
    }
}
