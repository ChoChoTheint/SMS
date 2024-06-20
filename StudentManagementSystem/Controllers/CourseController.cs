﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.DataModels;
using StudentManagementSystem.Models.ViewModels;

namespace StudentManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly SMSDbContext _dbContext;
        public CourseController(SMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        public IActionResult Entry()
        {
            var id = Guid.NewGuid().ToString();
            ViewBag.Id = id;

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Entry(CourseViewModel ui)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    CourseEntity courseData = new CourseEntity()
                    {
                        Id = ui.Id,
                        CreatedAt = DateTime.UtcNow,
                        IsInActive = true,
                        Name = ui.Name,
                        Description = ui.Description,
                        
                    };
                    _dbContext.Courses.Add(courseData);
                    _dbContext.SaveChanges();
                    TempData["info"] = "save successfully the record";
                }
                else
                {
                    //Reload id to populate the dropdown again
                    var id = Guid.NewGuid().ToString();
                    ViewBag.Id = id;

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
            IList<CourseViewModel> courseList = _dbContext.Courses.Select(s => new CourseViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                
            }).ToList();

            return View(courseList);
        }

        [Authorize]
        public IActionResult Detail()
        {
            IList<CourseViewModel> courseDetail = _dbContext.Courses.Select(s => new CourseViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,

            }).ToList();

            return View(courseDetail);
        }

        [Authorize]
        public IActionResult TeacherDetail(string Id)
        {
            IList<CourseViewModel> courseDetail = (from course in _dbContext.Courses
                                                   join tc in _dbContext.TeacherCourses
                                                   on course.Id equals tc.CourseId
                                                   join teacher in _dbContext.Teachers
                                                   on tc.TeacherId equals teacher.Id

                                                   where teacher.Email==Id
             select new CourseViewModel
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,

            }).ToList();

            return View(courseDetail);
        }

        [Authorize]
        public IActionResult StudentDetail(string Id)
        {
            IList<CourseViewModel> courseDetail = (from course in _dbContext.Courses
                                                   join batch in _dbContext.Batches
                                                   on course.Id equals batch.CourseId
                                                   join sb in _dbContext.StudentBatches
                                                   on batch.Id equals sb.BatchId
                                                   join student in _dbContext.Students
                                                   on sb.StudentId equals student.Id

                                                   where student.Email == Id
                                                   select new CourseViewModel
                                                   {
                                                       Id = course.Id,
                                                       Name = course.Name,
                                                       Description = course.Description,

                                                   }).ToList();

            return View(courseDetail);
        }


        [Authorize]
        public IActionResult Delete(string Id)
        {
            try
            {
                var deleteCourseData = _dbContext.Courses.Where(w => w.Id == Id).FirstOrDefault();
                if(deleteCourseData is not null)
                {
                    _dbContext.Courses.Remove(deleteCourseData);
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
            
                CourseViewModel editCourseData = _dbContext.Courses.Where(w => w.Id == Id).Select(s => new CourseViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    
                }).FirstOrDefault();

               
            return View(editCourseData);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(CourseViewModel ui)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    CourseEntity updateCourseData = new CourseEntity()
                    {
                        Id = ui.Id,
                        CreatedAt = DateTime.UtcNow,
                        ModifiedAt = DateTime.UtcNow,
                        IsInActive = true,
                        Name = ui.Name,
                        Description = ui.Description,
                        
                    };

                    _dbContext.Courses.Update(updateCourseData);
                    _dbContext.SaveChanges();
                    TempData["info"] = "update successfully the record";
                }
                else
                {
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
        public IActionResult Cancle(CourseViewModel ui)
        {
            CourseEntity course = new CourseEntity()
            {
                Name = "",
                Description = "",
            };
            return RedirectToAction("entry");
        }
    }
}
