﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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

        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> Entry(VideoViewModel ui) 
        {
            try
            {
                // Define the path to the uploads folder
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "videos");

                // Ensure the directory exists
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Generate a unique file name
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + ui.VideoFile.FileName;

                // Define the full path to the file
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save the uploaded file to the specified location
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ui.VideoFile.CopyToAsync(fileStream);
                }


                VideoEntity videoData = new VideoEntity()
                    {
                        Id = Guid.NewGuid().ToString(),
                        CreatedAt = DateTime.UtcNow,
                        IsInActive = true,
                        Name = ui.Name,
                        Description = ui.Description,
                        URL = "/videos" + uniqueFileName,
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

        [Authorize]
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
                                                   URL = video.URL,
                                                   CourseInfo = course.Name,
                                                   BatchInfo = batch.Name,
                                               }).ToList();
            return View(videoList);
        }

        [Authorize]
        public IActionResult Delete(string Id)
        {
            try
            {
                var deleteVideoData = _dbContext.Videos.Where(w => w.Id == Id).FirstOrDefault();
                if(deleteVideoData is not null)
                {
                    _dbContext.Videos.Remove(deleteVideoData);
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
            VideoViewModel editVideoData = _dbContext.Videos.Where(w => w.Id == Id).Select(s => new VideoViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                URL = s.URL,
<<<<<<< Updated upstream
=======
                CourseId = s.CourseId,
                BatchId = s.BatchId,
>>>>>>> Stashed changes

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

            return View(editVideoData);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(VideoViewModel ui)
        {
            try
            {
                VideoEntity updateVideData = new VideoEntity()
                {
                    Id = ui.Id,
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                    IsInActive = true,
                    Name = ui.Name,
                    Description = ui.Description,
                    URL = ui.URL,
                    CourseId = ui.CourseId,
                    BatchId = ui.BatchId,
                };
                _dbContext.Videos.Update(updateVideData);
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
