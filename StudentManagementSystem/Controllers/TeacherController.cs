using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.DataModels;
using StudentManagementSystem.Models.ViewModels;
using System.Security.Claims;

namespace StudentManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
        private readonly SMSDbContext _dbContext;
        public TeacherController(SMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        public IActionResult Entry()
        {

            ViewBag.AspUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewBag.Id = Guid.NewGuid().ToString();

            

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Entry(TeacherViewModel ui)
        {
           

            try
            {
                if (ModelState.IsValid)
                {

                    TeacherEntity teacherData = new TeacherEntity()
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
                        Position = ui.Position,
                        Gender = ui.Gender,
                        AspNetUsersId = ui.AspNetUsersId,
                    };
                    _dbContext.Teachers.Add(teacherData);
                    _dbContext.SaveChanges();
                    TempData["info"] = "save successfully the record";
                }
                else
                {
                    //Reload id and AspUserId to porpulate dropdown again

                    ViewBag.AspUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    ViewBag.Id = Guid.NewGuid().ToString();

                   

                    return View(ui);
                }
            }
            catch (Exception e)
            {
                TempData["info"] = "error while saving the record";
            }

            return RedirectToAction("list");
        }

        
        [Authorize]
        public IActionResult List()
        {
            IList<TeacherViewModel> teacherList = (from t in _dbContext.Teachers
                                                  

                                                   select new TeacherViewModel
                                                   {
                                                       Id = t.Id,
                                                       Name = t.Name,
                                                       Email = t.Email,
                                                       Phone = t.Phone,
                                                       Address = t.Address,
                                                       NRC = t.NRC,
                                                       DOB = t.DOB,
                                                       FatherName = t.Name,
                                                       Position = t.Position,
                                                       Gender = t.Gender,
                                                   }).ToList();
            return View(teacherList);
        }

        [Authorize]
        public IActionResult Delete(string Id)
        {
            try
            {
                var deleteData = _dbContext.Teachers.Where(w => w.Id == Id).FirstOrDefault();
                if (deleteData is not null)
                {
                    _dbContext.Teachers.Remove(deleteData);
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
                AspNetUsersId = s.AspNetUsersId,
            }).FirstOrDefault();

            

            return View(editTeacherData);
        }

        [Authorize]
        public IActionResult Update(TeacherViewModel ui)
        {

            try
            {
                if (ModelState.IsValid)
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
                        AspNetUsersId = ui.AspNetUsersId,
                    };

                    _dbContext.Teachers.Update(updateTeacherData);
                    _dbContext.SaveChanges();
                    TempData["info"] = "update successfully the record";
                }
                else
                {
                    return View("Edit", model: ui);
                }
            }
            catch (Exception e)
            {
                TempData["info"] = "error while updating the record";
            }

            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("List");
            }
            else
            {
                var teacherList = _dbContext.Teachers.Select(s => new TeacherViewModel
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

                var compositeModel = new CompositeViewModel
                {
                    Teachers = teacherList,
                };
                return View("~/Views/Home/TeacherIndex.cshtml", model: compositeModel);
            }
        }
    }
}
