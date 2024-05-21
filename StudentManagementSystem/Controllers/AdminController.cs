using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.DataModels;
using StudentManagementSystem.Models.ViewModels;

namespace StudentManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly SMSDbContext _dbContext;
        public AdminController(SMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Entry()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Entry(AdminViewModel ui)
        {
            try
            {
                AdminEntity data = new AdminEntity()
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
                    UserId = ui.UserId,
                };
                _dbContext.Admins.Add(data);
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
                var deleteData = _dbContext.Admins.Where(w => w.Id == Id).FirstOrDefault();

                if(deleteData is not null)
                {
                    _dbContext.Admins.Remove(deleteData);
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
            IList<AdminViewModel> editData = _dbContext.Admins.Where(w => w.Id == Id).Select(s => new AdminViewModel
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
            return View(editData);
        }

        [HttpPost]
        public IActionResult Update(AdminViewModel ui)
        {
            try
            {
                AdminEntity updateData = new AdminEntity()
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
                };

                _dbContext.Admins.Update(updateData);
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
