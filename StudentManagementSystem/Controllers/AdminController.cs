using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.DataModels;
using StudentManagementSystem.Models.ViewModels;
using System.Security.Claims;

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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                AdminEntity adminData = new AdminEntity()
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
                    AspNetUsersId = userId,
                };
                _dbContext.Admins.Add(adminData);
                _dbContext.SaveChanges();
                TempData["info"] = "save successfully the data";
            }
            catch(Exception e)
            {
                TempData["info"] = "error while saving the data";
            }
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            IList<AdminViewModel> adminList = _dbContext.Admins.Select(s => new AdminViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Phone = s.Phone,
                Address = s.Address,
                DOB = s.DOB,
                NRC = s.NRC,
                FatherName = s.FatherName,
                Gender = s.Gender,
            }).ToList();
            return View(adminList);
        }

        public IActionResult Delete(string Id)
        {
            try
            {
                var deleteAdminData = _dbContext.Admins.Where(w => w.Id == Id).FirstOrDefault();

                if(deleteAdminData is not null)
                {
                    _dbContext.Admins.Remove(deleteAdminData);
                    _dbContext.SaveChanges();
                }
                TempData["info"] = "delete successfully the data";
            }
            catch(Exception e)
            {
                TempData["info"] = "error while deleting the data";
            }
            return RedirectToAction("List");
        }

        public IActionResult Edit(string Id)
        {
            AdminViewModel editAdminData = _dbContext.Admins.Where(w => w.Id == Id).Select(s => new AdminViewModel
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
            }).FirstOrDefault();
            return View(editAdminData);
        }

        [HttpPost]
        public IActionResult Update(AdminViewModel ui)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                AdminEntity updateAdminData = new AdminEntity()
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
                    AspNetUsersId = userId,
                };

                _dbContext.Admins.Update(updateAdminData);
                _dbContext.SaveChanges();
                TempData["info"] = "update successfully the data";
            }
            catch(Exception e)
            {
                TempData["info"] = "error while updating the data";
            }
            return RedirectToAction("List");
        }
    }
}
