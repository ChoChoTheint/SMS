﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.DataModels;
using StudentManagementSystem.Models.ViewModels;

namespace StudentManagementSystem.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly SMSDbContext _dbContext;
        public AttendanceController(SMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
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
        public IActionResult Entry(AttendanceViewModel ui)
        {
            try
            {
                AttendanceEntity attendanceData = new AttendanceEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.UtcNow,
                    IsInActive = true,
                    AttendanceDate = ui.AttendanceDate,
                    InTime = ui.InTime,
                    OutTime = ui.OutTime,
                    IsLeave = ui.IsLeave,
                    StudentId = ui.StudentId,
                };
                _dbContext.Attendances.Add(attendanceData);
                _dbContext.SaveChanges();
                TempData["info"] = "save successfully the record";
            }
            catch (Exception e)
            {
                TempData["info"] = "error while saving the record";
            }
            return View();
        }

        public IActionResult List()
        {
            IList<AttendanceViewModel> attendanceList = (from attendance in _dbContext.Attendances
                                                         join student in _dbContext.Students
                                                         on attendance.StudentId equals student.Id
                                                         join batch in _dbContext.Batches
                                                         on student.BatchId equals batch.Id
                                                         select new AttendanceViewModel
                                                         {
                                                             Id = attendance.Id,
                                                             AttendanceDate = attendance.AttendanceDate,
                                                             InTime = attendance.InTime,
                                                             OutTime = attendance.OutTime,
                                                             IsLeave = attendance.IsLeave,
                                                             StudentInfo = student.Name + "/" + batch.Name
                                                         }).ToList();
            return View(attendanceList);
        }

        public IActionResult Delete(string Id)
        {
            try
            {
                var deleteAttendanceDate = _dbContext.Attendances.Where(w => w.Id == Id).FirstOrDefault();
                if(deleteAttendanceDate is not null)
                {
                    _dbContext.Attendances.Remove(deleteAttendanceDate);
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
            AttendanceViewModel editAttendanceData = _dbContext.Attendances.Where(w => w.Id == Id).Select(s => new AttendanceViewModel
                                            {
                                                Id = s.Id,
                                                AttendanceDate = s.AttendanceDate,
                                                InTime = s.InTime,
                                                OutTime = s.OutTime,
                                                IsLeave = s.IsLeave,
                                            }).FirstOrDefault();

            var students = _dbContext.Students.Select(s => new StudentViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Student = students;

            


            return View(editAttendanceData);
        }

        [HttpPost]
        public IActionResult Update(AttendanceViewModel ui)
        {
            try
            {
                AttendanceEntity updateAttendanceData = new AttendanceEntity()
                {
                    Id = ui.Id,
                    CreatedAt = DateTime.UtcNow,
                    IsInActive = true,
                    ModifiedAt = DateTime.UtcNow,
                    AttendanceDate = ui.AttendanceDate,
                    InTime = ui.InTime,
                    OutTime = ui.OutTime,
                    IsLeave = ui.IsLeave,
                    StudentId = ui.StudentId,
                };

                _dbContext.Attendances.Update(updateAttendanceData);
                _dbContext.SaveChanges();
                TempData["info"] = "update successfully the record";
            }
            catch (Exception e)
            {
                TempData["info"] = "error while updating the record";
            }
            return View("List");
        }
    }
}
