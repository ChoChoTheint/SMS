using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.DataModels;
using StudentManagementSystem.Models.ViewModels;

namespace StudentManagementSystem.Controllers
{
    public class BookController : Controller
    {
        private readonly SMSDbContext _dbContext;
        public BookController(SMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        public IActionResult Entry()
        {
            var courses = _dbContext.Courses.Select(s => new CourseViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Course = courses;

            var batches = _dbContext.Batches.Select(s => new BatchViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Batch = batches;


            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Entry(BookViewModel ui)
        {
            try
            {
                BookEntity data = new BookEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.UtcNow,
                    IsInActive = true,
                    Name = ui.Name,
                    Description = ui.Description,
                    CourseId = ui.CourseId,
                    BatchId = ui.BatchId,
                };
                _dbContext.Books.Add(data);
                _dbContext.SaveChanges();
                TempData["info"] = "save successfully the record";
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
            IList<BookViewModel> bookList = (from book in _dbContext.Books
                                             join course in _dbContext.Courses
                                             on book.CourseId equals course.Id
                                             join batch in _dbContext.Batches
                                             on book.BatchId equals batch.Id
                                             select new BookViewModel
                                             {
                                                 Id = book.Id,
                                                 Name = book.Name,
                                                 Description = book.Description,
                                                 CourseInfo = course.Name,
                                                 BatchInfo = batch.Name
                                             }).ToList();
            return View(bookList);
        }

        [Authorize]
        public IActionResult Delete(string Id)
        {
            try
            {
                var deleteData = _dbContext.Books.Where(w => w.Id == Id).FirstOrDefault();
                if(deleteData is not null)
                {
                    _dbContext.Books.Remove(deleteData);
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
                BookViewModel editBookData = _dbContext.Books.Where(w => w.Id == Id).Select(s => new BookViewModel
                                      {
                                          Id = s.Id,
                                          Name = s.Name,
                                          Description = s.Description,
                                          CourseId = s.CourseId,
                                          BatchId = s.BatchId,
                                      }).FirstOrDefault();

            var courses = _dbContext.Courses.Select(s => new CourseViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Course = courses;

            var batches = _dbContext.Batches.Select(s => new BatchViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Batch = batches;



            return View(editBookData);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(BookViewModel ui)
        {
            try
            {
                BookEntity updateBookData = new BookEntity()
                {
                    Id = ui.Id,
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                    IsInActive = true,
                    Name = ui.Name,
                    Description = ui.Description,
                    CourseId = ui.CourseId,
                    BatchId = ui.BatchId,
                };
                _dbContext.Books.Update(updateBookData);
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
