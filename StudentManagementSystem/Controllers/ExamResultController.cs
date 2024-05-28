using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.DataModels;
using StudentManagementSystem.Models.ViewModels;

namespace StudentManagementSystem.Controllers
{
    public class ExamResultController : Controller
    {
        private readonly SMSDbContext _dbContext;
        public ExamResultController(SMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        public IActionResult Entry()
        {
            var students = _dbContext.Students.Select(s => new StudentViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Student = students;

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Entry(ExamResultViewModel ui)
        {
            try
            {
                ExamResultEntity examResultData = new ExamResultEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.UtcNow,
                    IsInActive = true,
                    Mark = ui.Mark,
                    StudentId = ui.StudentId,
                };
                _dbContext.ExamResults.Add(examResultData);
                _dbContext.SaveChanges();
                TempData["info"] = "save successfully the record";
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
            IList<ExamResultViewModel> examResultList = (from examResult in _dbContext.ExamResults
                                                      join student in _dbContext.Students
                                                      on examResult.StudentId equals student.Id
                                                      join batch in _dbContext.Batches
                                                      on student.BatchId equals batch.Id

                                                      select new ExamResultViewModel
                                                      {
                                                          Id = examResult.Id,
                                                          Mark = examResult.Mark,
                                                          StudentInfo = student.Name+"/ "+batch.Name
                                                      }).ToList();
            return View(examResultList);
        }

        [Authorize]
        public IActionResult Delete(string Id)
        {
            try
            {
                var deleteExamResultData = _dbContext.ExamResults.Where(w => w.Id == Id).FirstOrDefault();
                if(deleteExamResultData is not null)
                {
                    _dbContext.ExamResults.Remove(deleteExamResultData);
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
            ExamResultViewModel editExamResultData = _dbContext.ExamResults.Where(w => w.Id == Id).Select(s => new ExamResultViewModel
            {
                Id = s.Id,
                Mark = s.Mark,
            }).FirstOrDefault();

            var students = _dbContext.Students.Select(s => new StudentViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Student = students;

            return View(editExamResultData);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(ExamResultViewModel ui)
        {
            try
            {
                ExamResultEntity updateExamResultData = new ExamResultEntity()
                {
                    Id = ui.Id,
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                    IsInActive = true,
                    Mark = ui.Mark,
                    StudentId = ui.StudentId,
                };
                _dbContext.ExamResults.Update(updateExamResultData);
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
