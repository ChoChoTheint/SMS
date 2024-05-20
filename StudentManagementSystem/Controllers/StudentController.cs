using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.DataModels;
using StudentManagementSystem.Models.ViewModels;

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
            return View();
        }

        [HttpPost]
        public IActionResult Entry(StudentViewModel ui)
        {
            try
            {
                StudentEntity data = new StudentEntity()
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
                };
                _dbContext.Students.Add(data);
                _dbContext.SaveChanges();
                TempData["info"] = "save successfully data";
            }
            catch(Exception e)
            {
                TempData["info"] = "error while saving data";
            }
            return View();
        }

        public IActionResult Delete(string Id)
        {
            try
            {
                var deleteData = _dbContext.Students.Where(w => w.Id == Id).FirstOrDefault();
                if(deleteData is not null)
                {
                    _dbContext.Students.Remove(deleteData);
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
            IList<StudentViewModel> editData = _dbContext.Students.Where(w => w.Id == Id).Select(s => new StudentViewModel
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
                BatchId = s.BatchId,
            }).ToList();
            return View(editData);
        }

        [HttpPost]
        public IActionResult Update(StudentViewModel ui)
        {
            try
            {
                StudentEntity updateData = new StudentEntity()
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

                _dbContext.Students.Update(updateData);
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
