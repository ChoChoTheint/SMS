using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [Authorize]
        public IActionResult Entry()
        {
            ViewBag.Id = Guid.NewGuid().ToString();
            var students = (from student in _dbContext.Students
                            join sb in _dbContext.StudentBatches
                           on student.Id equals sb.StudentId
                           join batch in _dbContext.Batches
                           on sb.BatchId equals batch.Id
                           join course in _dbContext.Courses
                           on batch.CourseId equals course.Id

                           select new StudentViewModel
                           {
                               Id = student.Id,
                               Name = student.Name + "/ " + batch.Name+"/ "+course.Name
                           }).OrderBy(o => o.Name).ToList();
            ViewBag.Student = students;
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Entry(AttendanceViewModel ui)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    AttendanceEntity attendanceData = new AttendanceEntity()
                    {
                        Id = ui.Id,
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
                    TempData["info"] = "save successfully data";
                }
                else
                {

                    ViewBag.Id = Guid.NewGuid().ToString();
                    var students = (from student in _dbContext.Students
                                    join sb in _dbContext.StudentBatches
                                   on student.Id equals sb.StudentId
                                    join batch in _dbContext.Batches
                                    on sb.BatchId equals batch.Id
                                    join course in _dbContext.Courses
                                    on batch.CourseId equals course.Id

                                    select new StudentViewModel
                                    {
                                        Id = student.Id,
                                        Name = student.Name + "/ " + batch.Name + "/ " + course.Name
                                    }).OrderBy(o => o.Name).ToList();
                    ViewBag.Student = students;
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
            IList<AttendanceViewModel> attendanceList = (from attendance in _dbContext.Attendances
                                                         join sb in _dbContext.StudentBatches
                                                         on attendance.StudentId equals sb.StudentId
                                                         join student in _dbContext.Students
                                                         on sb.StudentId equals student.Id
                                                         join batch in _dbContext.Batches
                                                         on sb.BatchId equals batch.Id
                                                         join course in _dbContext.Courses
                                                         on batch.CourseId equals course.Id

                                                         where attendance.StudentId == student.Id

                                                         select new AttendanceViewModel
                                                         {
                                                             Id = attendance.Id,
                                                             AttendanceDate = attendance.AttendanceDate,
                                                             InTime = attendance.InTime,
                                                             OutTime = attendance.OutTime,
                                                             IsLeave = attendance.IsLeave,
                                                             StudentId = student.Name+"/ "+ batch.Name+"/ "+course.Name, 
                                                         }).ToList();

            
            return View(attendanceList);
        }

        [Authorize]
        public IActionResult Detail()
        {
            IList<AttendanceViewModel> attendanceDetail = (from attendance in _dbContext.Attendances
                                                         join sb in _dbContext.StudentBatches
                                                         on attendance.StudentId equals sb.StudentId
                                                         join student in _dbContext.Students
                                                         on sb.StudentId equals student.Id
                                                         join batch in _dbContext.Batches
                                                         on sb.BatchId equals batch.Id
                                                         join course in _dbContext.Courses
                                                         on batch.CourseId equals course.Id

                                                         where attendance.StudentId == student.Id

                                                         select new AttendanceViewModel
                                                         {
                                                             Id = attendance.Id,
                                                             AttendanceDate = attendance.AttendanceDate,
                                                             InTime = attendance.InTime,
                                                             OutTime = attendance.OutTime,
                                                             IsLeave = attendance.IsLeave,
                                                             StudentId = student.Name + "/ " + batch.Name + "/ " + course.Name,
                                                         }).ToList();
            return View(attendanceDetail);
        }

        [Authorize]
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

        [Authorize]
        public IActionResult Edit(string Id)
        {
            AttendanceViewModel editAttendanceData = _dbContext.Attendances.Where(w => w.Id == Id).Select(s => new AttendanceViewModel
                                            {
                                                Id = s.Id,
                                                AttendanceDate = s.AttendanceDate,
                                                InTime = s.InTime,
                                                OutTime = s.OutTime,
                                                IsLeave = s.IsLeave,
                                                StudentId = s.StudentId,
                                            }).FirstOrDefault();

            var students = (from student in _dbContext.Students
                            join sb in _dbContext.StudentBatches
                           on student.Id equals sb.StudentId
                            join batch in _dbContext.Batches
                            on sb.BatchId equals batch.Id
                            join course in _dbContext.Courses
                            on batch.CourseId equals course.Id

                            select new StudentViewModel
                            {
                                Id = student.Id,
                                Name = student.Name + "/ " + batch.Name + "/ " + course.Name
                            }).OrderBy(o => o.Name).ToList();
            ViewBag.Student = students;

            return View(editAttendanceData);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(AttendanceViewModel ui)
        {
            try
            {
                if (!ModelState.IsValid)
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
                else
                {
                    var students = (from student in _dbContext.Students
                                    join sb in _dbContext.StudentBatches
                                   on student.Id equals sb.StudentId
                                    join batch in _dbContext.Batches
                                    on sb.BatchId equals batch.Id
                                    join course in _dbContext.Courses
                                    on batch.CourseId equals course.Id

                                    select new StudentViewModel
                                    {
                                        Id = student.Id,
                                        Name = student.Name + "/ " + batch.Name + "/ " + course.Name
                                    }).OrderBy(o => o.Name).ToList();
                    ViewBag.Student = students;

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
