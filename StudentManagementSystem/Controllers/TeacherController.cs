using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.DataModels;
using StudentManagementSystem.Models.ViewModels;

namespace StudentManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
        private readonly SMSDbContext _dbContext;
        public TeacherController(SMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Entry()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entry(TeacherViewModel ui)
        {
            try
            {
                TeacherEntity teacherData = new TeacherEntity()
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
                    Position = ui.Position,
                    Gender = ui.Gender,
                    UserId = ui.UserId,
                };
                _dbContext.Teachers.Add(teacherData);
                _dbContext.SaveChanges();
                TempData["info"] = "save successfully the record";
            }
            catch (Exception e)
            {
                TempData["info"] = "error while saving the record";
            }
            return RedirectToAction("List");
        }

        public IActionResult Delete(string Id)
        {
            try
            {
                var deleteTeacherData = _dbContext.Teachers.Where(w => w.Id == Id).FirstOrDefault();
                if(deleteTeacherData is not null)
                {
                    _dbContext.Teachers.Remove(deleteTeacherData);
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
            TeacherViewModel editTeacherData = _dbContext.Teachers.Where(w => w.Id == Id).Select(s => new TeacherViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Phone = s.Phone,
                Address = s.Address,
                NRC = s.NRC,
                DOB = s.DOB,
                FatherName = s.FatherName,
                Position = s.Position,
                Gender = s.Gender,
                UserId = s.UserId,
            }).FirstOrDefault();
            return View(editTeacherData);
        }

        public IActionResult Update(TeacherViewModel ui)
        {
            try
            {
                TeacherEntity updateTeacherData = new TeacherEntity()
                {
                    Id = ui.Id,
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                    IsInActive = true,
                    Name = ui.Name,
                    Email = ui.Email,
                    Phone = ui.Phone,
                    Address = ui.Address,
                    NRC = ui.NRC,
                    DOB = ui.DOB,
                    FatherName = ui.FatherName,
                    Position = ui.Position,
                    Gender = ui.Gender,
                    UserId = ui.UserId,
                };

                _dbContext.Teachers.Update(updateTeacherData);
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
