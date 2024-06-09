using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.DataModels;
using StudentManagementSystem.Models.ViewModels;
using System.Security.Claims;

namespace StudentManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly SMSDbContext _dbContext;
        public StudentController(SMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        public IActionResult Entry()
        {
            ViewBag.Id = Guid.NewGuid().ToString();

            ViewBag.AspUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

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

        [HttpPost]
        [Authorize]
        public IActionResult Entry(StudentViewModel ui)
        {
            

            try
            {
                if (ModelState.IsValid)
                {

                    StudentEntity studentData = new StudentEntity()
                    {
                        Id = ui.Id,
                        CreatedAt = DateTime.UtcNow,
                        IsInActive = true,
                        Name = ui.Name,
                        Email = ui.Email,
                        Phone = ui.Phone,
                        Address = ui.Address,
                        NRC = ui.NRC,
                        DOB = ui.DOB,
                        FatherName = ui.FatherName,
                        Gender = ui.Gender,
                        BatchId = ui.BatchId,
                        AspNetUsersId = ui.AspNetUsersId,
                    };
                    _dbContext.Students.Add(studentData);
                    _dbContext.SaveChanges();
                    TempData["info"] = "save successfully data";
                }
                else
                {
                    //Relodad Id,AspUserId and BatchId to populat the dropdown again

                    ViewBag.Id = Guid.NewGuid().ToString();

                    ViewBag.AspUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    var batches = (from batch in _dbContext.Batches
                                   join course in _dbContext.Courses
                                   on batch.CourseId equals course.Id

                                   select new BatchViewModel
                                   {
                                       Id = batch.Id,
                                       Name = batch.Name + "/ " + course.Name
                                   }).OrderBy(o => o.Name).ToList();
                    ViewBag.Batch = batches;

                    return View(ui);
                }
            }
            catch(Exception e)
            {
                TempData["info"] = "error while saving data";
            }
            return RedirectToAction("list");
        }

        [Authorize]
        public IActionResult List()
        {
            IList<StudentViewModel> studentList = (from student in _dbContext.Students
                                                   join batch in _dbContext.Batches
                                                   on student.BatchId equals batch.Id
                                                   join course in _dbContext.Courses
                                                   on batch.CourseId equals course.Id

                                                   select new StudentViewModel
                                                   {
                                                       Id = student.Id,
                                                       Name = student.Name,
                                                       Email = student.Email,
                                                       Phone = student.Phone,
                                                       Address = student.Address,
                                                       NRC = student.NRC,
                                                       DOB = student.DOB,
                                                       FatherName = student.FatherName,
                                                       Gender = student.Gender,
                                                       BatchId = batch.Name,
                                                       CourseId = course.Name,
                                                   }).ToList();
            return View(studentList);
        }

        [Authorize]
        public IActionResult Delete(string Id)
        {
            try
            {
                var deleteStudentData = _dbContext.Students.FirstOrDefault(w => w.Id == Id);
                if(deleteStudentData is not null)
                {
                    _dbContext.Students.Remove(deleteStudentData);
                    _dbContext.SaveChanges();
                }
                TempData["info"] = "delete successfully data";
            }
            catch(Exception e)
            {
                TempData["info"] = "error while deleting data";
            }
            return RedirectToAction("list");
        }

        [Authorize]
        public IActionResult Edit(string Id)
        {
            StudentViewModel editStudentData = _dbContext.Students.Where(w => w.Id == Id).Select(s => new StudentViewModel
            {
                Id = s.Id,
                AspNetUsersId = s.AspNetUsersId,
                Name = s.Name,
                Email = s.Email,
                Phone = s.Phone,
                Address = s.Address,
                NRC = s.NRC,
                DOB = s.DOB,
                FatherName = s.FatherName,
                Gender = s.Gender,
                BatchId = s.BatchId
            }).FirstOrDefault();


            var batches = (from batch in _dbContext.Batches
                           join course in _dbContext.Courses
                           on batch.CourseId equals course.Id

                           select new BatchViewModel
                           {
                               Id = batch.Id,
                               Name = batch.Name + "/ " + course.Name
                           }).OrderBy(o => o.Name).ToList();
            ViewBag.Batch = batches;

            return View(editStudentData);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(StudentViewModel ui)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    StudentEntity updateStudentData = new StudentEntity()
                    {
                        Id = ui.Id,
                        CreatedAt = DateTime.UtcNow,
                        ModifiedAt = DateTime.UtcNow,
                        Name = ui.Name,
                        Email = ui.Email,
                        Phone = ui.Phone,
                        Address = ui.Address,
                        NRC = ui.NRC,
                        DOB = ui.DOB,
                        FatherName = ui.FatherName,
                        Gender = ui.Gender,
                        AspNetUsersId = ui.AspNetUsersId,
                        BatchId = ui.BatchId,
                    };

                    _dbContext.Students.Update(updateStudentData);
                    _dbContext.SaveChanges();
                    TempData["info"] = "update successfully data";
                }
                else
                {
                    var batches = (from batch in _dbContext.Batches
                                   join course in _dbContext.Courses
                                   on batch.CourseId equals course.Id

                                   select new BatchViewModel
                                   {
                                       Id = batch.Id,
                                       Name = batch.Name + "/ " + course.Name
                                   }).OrderBy(o => o.Name).ToList();
                    ViewBag.Batch = batches;

                    return View("Edit", model: ui);
                }
            }
            catch(Exception e)
            {
                TempData["info"] = "error while updating data";
            }
            return RedirectToAction("list");
        }
    }
}
