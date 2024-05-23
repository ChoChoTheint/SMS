﻿using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.DataModels;
using StudentManagementSystem.Models.ViewModels;

namespace StudentManagementSystem.Controllers
{
    public class BatchController : Controller
    {
        private readonly SMSDbContext _dbContext;
        public BatchController(SMSDbContext dbContext)
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

            return View();
        }


        [HttpPost]
        public IActionResult Entry(BatchViewModel ui)
        {
            try
            {

                BatchEntity batchData = new BatchEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.UtcNow,
                    IsInActive = true,
                    Name = ui.Name,
                    Description = ui.Description,
                    CourseId = ui.CourseId,
                };
                _dbContext.Batches.Add(batchData);
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
            IList<BatchViewModel> batchList = (from batch in _dbContext.Batches
                                               join course in _dbContext.Courses
                                               on batch.CourseId equals course.Id

                                               select new BatchViewModel
                                               {
                                                   Id = batch.Id,
                                                   Name = batch.Name,
                                                   Description = batch.Description,
                                                   CourseInfo = course.Name,
                                               }).ToList();
            return View(batchList);
        }

        public IActionResult Delete(string Id)
        {
            try
            {
                var deleteBatchData = _dbContext.Batches.Where(w => w.Id == Id).FirstOrDefault();
                if(deleteBatchData is not null)
                {
                    _dbContext.Batches.Remove(deleteBatchData);
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
            BatchViewModel editBatchData = _dbContext.Batches.Where(w => w.Id == Id).Select(s => new BatchViewModel
            {
                Id = s.Id,
                Description = s.Description,
                CourseId = s.CourseId,
            }).FirstOrDefault();

            var courses = _dbContext.Courses.Select(s => new CourseViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Course = courses;

            return View(editBatchData);
        }

        [HttpPost]
        public IActionResult Update(BatchViewModel ui)
        {
            try
            {
                BatchEntity updateBatchData = new BatchEntity()
                {
                    Id = ui.Id,
                    CreatedAt   = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                    IsInActive = true,
                    Name = ui.Name,
                    Description = ui.Description,
                    CourseId = ui.CourseId,
                };

                _dbContext.Batches.Update(updateBatchData);
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
