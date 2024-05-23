using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.DataModels;
using StudentManagementSystem.Models.ViewModels;

namespace StudentManagementSystem.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly SMSDbContext _dbContext;
        public AssignmentController(SMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Entry()
        {
            var courses = _dbContext.Courses.Select(s => new CourseViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Course = courses;

            var batches = _dbContext.Batches.Select(s => new BatchViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Batch = batches;

            return View();
        }

        [HttpPost]
        public IActionResult Entry(AssignmentViewModel ui)
        {
            try
            {
                AssignmentEntity assignmentData = new AssignmentEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.UtcNow,
                    IsInActive = true,
                    Name = ui.Name,
                    Description = ui.Description,
                    URL = ui.URL,
                    CourseId = ui.CourseInfo,
                    BatchId = ui.BatchId,
                };
                _dbContext.Assignments.Add(assignmentData);
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
            IList<AssignmentViewModel> assignmentList = (from assignment in _dbContext.Assignments
                                                         join course in _dbContext.Courses
                                                         on assignment.CourseId equals course.Id
                                                         join batch in _dbContext.Batches
                                                         on assignment.BatchId equals batch.Id

                                                         select new AssignmentViewModel
                                                         {
                                                             Id = assignment.Id,
                                                             Name = assignment.Name,
                                                             Description = assignment.Description,
                                                             URL = assignment.URL,
                                                             CourseInfo = course.Name,
                                                             BatchInfo = batch.Name,
                                                         }).ToList();
            return View(assignmentList);
        }

        public IActionResult Delete(string Id)
        {
            try
            {
                var deleteAssignmentData = _dbContext.Assignments.Where(w => w.Id == Id).FirstOrDefault();
                if(deleteAssignmentData is not null)
                {
                    _dbContext.Assignments.Remove(deleteAssignmentData);
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
            AssignmentViewModel editAddignmentData = _dbContext.Assignments.Where(w => w.Id == Id).Select(s => new AssignmentViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                URL = s.URL,
                CourseId = s.CourseId,
                BatchId = s.BatchId,
            }).FirstOrDefault();


            var courses = _dbContext.Courses.Select(s => new CourseViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Course = courses;

            var batches = _dbContext.Batches.Select(s => new BatchViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Batch = batches;

            return View(editAddignmentData);
        }

        [HttpPost]
        public IActionResult Update(AssignmentViewModel ui)
        {
            try
            {
                AssignmentEntity updateAssignmentData = new AssignmentEntity()
                {
                    Id = ui.Id,
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                    IsInActive = true,
                    Name = ui.Name,
                    Description = ui.Description,
                    URL = ui.URL,
                    CourseId = ui.CourseInfo,
                    BatchId = ui.BatchId,
                };
                _dbContext.Assignments.Update(updateAssignmentData);
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
