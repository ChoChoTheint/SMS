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


            var courses = _dbContext.Courses.Select(s => new CourseViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Course = courses;

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
            IList<StudentViewModel> studentList = _dbContext.Students.Select(s => new StudentViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Phone = s.Phone,
                Address = s.Address,
                NRC = s.NRC,
                DOB = s.DOB,
                FatherName = s.FatherName,
                Gender = s.Gender,
            }).ToList();

            return  View(studentList);
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

            }).FirstOrDefault();


            return View(editStudentData);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(StudentViewModel ui)
        {
            try
            {
                if (ModelState.IsValid)
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

                    };

                    _dbContext.Students.Update(updateStudentData);
                    _dbContext.SaveChanges();
                    TempData["info"] = "update successfully data";
                }
                else
                {
                    return View("Edit", model: ui);
                }
            }
            catch(Exception e)
            {
                TempData["info"] = "error while updating data";
            }
            if (User.IsInRole("admin"))
            {
                return RedirectToAction("list");
            }
            else
            {
                 return View("~/Views/Home/StudentIndex.cshtml");
            }
            
        }
    }
}
