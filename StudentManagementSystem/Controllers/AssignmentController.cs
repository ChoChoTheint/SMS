using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.DataModels;
using StudentManagementSystem.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;


namespace StudentManagementSystem.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly SMSDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AssignmentController(SMSDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        public IActionResult Entry()
        {
            var courses = _dbContext.Courses.Select(s => new CourseViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Course = courses;

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
        private string UploadedFile(AssignmentViewModel model)
        {
            string uniqueFileName = null;

            if (model.File != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "files");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.File.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
       // private string UploadedFile(AssignmentViewModel model)
        //{
          //  string uniqueFileName = null;

           // if (model.File != null)
            //{
              //  string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "files");
              //  uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
              //  string filePath = Path.Combine(uploadsFolder, uniqueFileName);
              //  using (var fileStream = new FileStream(filePath, FileMode.Create))
              //  {
                //    model.File.CopyTo(fileStream);
              //  }
          //  }
          //  return uniqueFileName;
      //  }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Entry(AssignmentViewModel ui)
        {
            try
            {
                // Define the path to the uploads folder
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "files");

                // Ensure the directory exists
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Generate a unique file name
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + ui.File.FileName;

                // Define the full path to the file
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save the uploaded file to the specified location
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ui.File.CopyToAsync(fileStream);
                }

                if (!ModelState.IsValid)
                {

                    AssignmentEntity assignmentData = new AssignmentEntity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            CreatedAt = DateTime.UtcNow,
                            IsInActive = true,
                            Name = ui.Name,
                            Description = ui.Description,
                            URL = uniqueFileName,
                            CourseId = ui.CourseId,
                            BatchId = ui.BatchId,
                        };
                        _dbContext.Assignments.Add(assignmentData);
                        _dbContext.SaveChanges();
                        TempData["info"] = "save successfully the record";
                }
                if (ModelState.IsValid)
                {
                    return View(ui);
                }
                
            }
            catch (Exception e)
            {
                TempData["info"] = "error while saving the record";
            }
            return RedirectToAction("list");
        }

        [Authorize]
        private MemoryStream FilePath(string fileName)
        {

            string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "files");

            var folder = uploadFolder;

            if (fileName == folder)
            {
                fileName = folder;
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), uploadFolder, fileName);
            var memeory = new MemoryStream();

            if (System.IO.File.Exists(path))
            {
                var net = new System.Net.WebClient();
                var data = net.DownloadData(path);
                var content = new System.IO.MemoryStream(data);
                memeory = content;
            }
            memeory.Position = 0;
            return memeory;
        }
        public IActionResult DownloadFile(string filePath)
        {
            var memory = FilePath(filePath);
            return File(memory.ToArray(), "application/pdf", filePath);
        }
        
        [Authorize]
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

        [Authorize]
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
            return RedirectToAction("list");
        }

        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> Update(AssignmentViewModel ui)
        {
            try
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "files");

                // Ensure the directory exists
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Generate a unique file name
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + ui.File.FileName;

                // Define the full path to the file
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save the uploaded file to the specified location
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ui.File.CopyToAsync(fileStream);
                }
                AssignmentEntity updateAssignment = new AssignmentEntity()
                {
                    Id = ui.Id,
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                    IsInActive = true,
                    Name = ui.Name,
                    Description = ui.Description,
                    URL = uniqueFileName,
                    CourseId = ui.CourseId,
                    BatchId = ui.BatchId,
                };
                _dbContext.Assignments.Update(updateAssignment);
                _dbContext.SaveChanges();
                TempData["info"] = "update successfully the record";
            }
            catch (Exception e)
            {
                TempData["info"] = "error while updating the record";
            }
            return RedirectToAction("list");
        }
    }
}
