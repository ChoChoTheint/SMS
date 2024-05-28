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
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Entry(ExamViewModel ui)
        {
            try
            {
                ExamEntity examData = new ExamEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.UtcNow,
                    IsInActive = true,
                    Name = ui.Name,
                    ExamDate = ui.ExamDate,
                };
                _dbContext.Exams.Add(examData);
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
            IList<ExamViewModel> examList = _dbContext.Exams.Select(s => new ExamViewModel
            {
                Id = s.Id,
                Name = s.Name,
                ExamDate = s.ExamDate,
            }).ToList();

            return View(examList);
        }

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
                ExamDate = s.ExamDate,
            }).FirstOrDefault();
            return View(editExamData);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(ExamViewModel ui)
        {
            try
            {
                ExamEntity updateExamData = new ExamEntity()
                {
                    Id = ui.Id,
                    CreatedAt = DateTime.UtcNow,
                    IsInActive = true,
                    ModifiedAt = DateTime.UtcNow,
                    Name = ui.Name,
                    ExamDate = ui.ExamDate,
                };
                _dbContext.Exams.Update(updateExamData);
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
