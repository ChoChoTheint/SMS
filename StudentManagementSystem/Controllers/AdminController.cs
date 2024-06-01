using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public IActionResult Entry()
        {
            var aspUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.AspUserId = aspUserId;

            var id = Guid.NewGuid().ToString();
            ViewBag.Id = id;

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Entry(AdminViewModel ui)
        {
            

            try
            {
                if (ModelState.IsValid)
                {

                    AdminEntity adminData = new AdminEntity()
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
                    _dbContext.Admins.Add(adminData);
                    _dbContext.SaveChanges();
                    TempData["info"] = "save successfully the data";
                }
                if (!ModelState.IsValid)
                {
                    var aspUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    ViewBag.AspUserId = aspUserId;

                    var id = Guid.NewGuid().ToString();
                    ViewBag.Id = id;

                    return View(ui);
                }

            }
            catch (Exception e)
            {
                TempData["info"] = "error while saving the data";
            }
            return RedirectToAction("List");
        }

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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
        [Authorize]
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
