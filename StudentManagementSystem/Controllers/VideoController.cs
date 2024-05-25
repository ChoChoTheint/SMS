using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.DataModels;
using StudentManagementSystem.Models.ViewModels;
using System.IO;
using System.Threading.Tasks;

namespace StudentManagementSystem.Controllers
{
    public class VideoController : Controller
    {
        private readonly SMSDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public VideoController(SMSDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
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

        private string UploadedFile(VideoViewModel model)
        {
            string uniqueFileName = null;

            if (model.URL != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "videos");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.URL.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
               using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.URL.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        [HttpPost]
        public IActionResult Entry(VideoViewModel ui) 
        {
            try
            {
               

                    VideoEntity videoData = new VideoEntity()
                    {
                        Id = Guid.NewGuid().ToString(),
                        CreatedAt = DateTime.UtcNow,
                        IsInActive = true,
                        Name = ui.Name,
                        Description = ui.Description,
                        URL = UploadedFile(ui),
                        CourseId = ui.CourseId,
                        BatchId = ui.BatchId,
                    };
                    _dbContext.Videos.Add(videoData);
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
            IList<VideoViewModel> videoList = (from video in _dbContext.Videos
                                               join course in _dbContext.Courses
                                               on video.CourseId equals course.Id
                                               join batch in _dbContext.Batches
                                               on video.BatchId equals batch.Id
                                               select new VideoViewModel
                                               {
                                                   Id = video.Id,
                                                   Name = video.Name,
                                                   Description = video.Description,
                                                   Video = video.URL,
                                                   CourseInfo = course.Name,
                                                   BatchInfo = batch.Name,
                                               }).ToList();
            return View(videoList);
        }
    }
}
