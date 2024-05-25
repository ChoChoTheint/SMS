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
        public IActionResult Entry()
        {
            var batches = _dbContext.Batches.Select(s => new BatchViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Batch = batches;

            return View();
        }

        [HttpPost]
        public IActionResult Entry(StudentViewModel ui)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                StudentEntity studentData = new StudentEntity()
                {
                    Id = Guid.NewGuid().ToString(),
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
                    AspNetUsersId = userId,
                };
                _dbContext.Students.Add(studentData);
                _dbContext.SaveChanges();
                TempData["info"] = "save successfully data";
            }
            catch(Exception e)
            {
                TempData["info"] = "error while saving data";
            }
            return View();
        }

        public IActionResult List()
        {
            IList<StudentViewModel> studentList = (from student in _dbContext.Students
                                                   join batch in _dbContext.Batches
                                                   on student.BatchId equals batch.Id

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
                                                       BatchInfo = batch.Name
                                                   }).ToList();
            return View(studentList);
        }
        public IActionResult Delete(string Id)
        {
            try
            {
                var deleteStudentData = _dbContext.Students.Where(w => w.Id == Id).FirstOrDefault();
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
            return View();
        }

        public IActionResult Edit(string Id)
        {
            IList<StudentViewModel> editStudentData = _dbContext.Students.Where(w => w.Id == Id).Select(s => new StudentViewModel
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

            var batches = _dbContext.Batches.Select(s => new BatchViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Batch = batches;

            return View(editStudentData);
        }

        [HttpPost]
        public IActionResult Update(StudentViewModel ui)
        {
            try
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
                    BatchId = ui.BatchId,
                };

                _dbContext.Students.Update(updateStudentData);
                _dbContext.SaveChanges();
                TempData["info"] = "update successfully data";
            }
            catch(Exception e)
            {
                TempData["info"] = "error while updating data";
            }
            return View();
        }
    }
}
