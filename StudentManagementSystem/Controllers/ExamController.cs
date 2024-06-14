﻿using Microsoft.AspNetCore.Authorization;
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
            IList<ExamViewModel> examList = _dbContext.Exams.Select(s => new ExamViewModel
            {
                Id = s.Id,
                Name = s.Name,
                ExamDate = s.ExamDate,
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
                if (ModelState.IsValid)
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
    }
}
