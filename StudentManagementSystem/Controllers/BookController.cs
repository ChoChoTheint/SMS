using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.DataModels;
using StudentManagementSystem.Models.ViewModels;
using StudentManagementSystem.Utilities;
using System;

namespace StudentManagementSystem.Controllers
{
    public class BookController : Controller
    {
        private readonly SMSDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PdfFileReader _pdfFileReader;
        public BookController(SMSDbContext dbContext, IWebHostEnvironment webHostEnvironment, PdfFileReader pdfFileReader)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            _pdfFileReader = pdfFileReader;
        }

        [Authorize]
        public IActionResult Entry()
        {
            ViewBag.Id = Guid.NewGuid().ToString();

            var courses = _dbContext.Courses.Select(s => new CourseViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Course = courses;

            var batches = (from batch in _dbContext.Batches
                           join course in _dbContext.Courses
                           on batch.CourseId equals course.Id
                           
                           select new BatchViewModel
                           {
                               Id = batch.Id,
                               Name = batch.Name+"/ "+course.Name
                           }).OrderBy(o => o.Name).ToList();
            ViewBag.Batch = batches;


            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Entry(BookViewModel ui)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Define the path to the uploads folder
                    var uploadFolder = Path.Combine("wwwroot", "books");

                    // Ensure the directory exists
                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    // Generate a unique file name
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + ui.File.FileName;

                    // Define the full path to the file
                    var filePath = Path.Combine(uploadFolder, uniqueFileName);

                    // Save the uploaded file to the specified location
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ui.File.CopyToAsync(fileStream);
                    }
                    BookEntity data = new BookEntity()
                    {
                        Id = ui.Id,
                        CreatedAt = DateTime.UtcNow,
                        IsInActive = true,
                        Name = ui.Name,
                        Description = ui.Description,
                        URL = uniqueFileName,
                        CourseId = ui.CourseId,
                        BatchId = ui.BatchId,
                    };
                    _dbContext.Books.Add(data);
                    _dbContext.SaveChanges();
                    TempData["info"] = "save successfully the record";
                }
                else
                {
                    //Reload Id, CourseId and BatchId to populate the dropdown again

                    ViewBag.Id = Guid.NewGuid().ToString();

                    var courses = _dbContext.Courses.Select(s => new CourseViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                    }).OrderBy(o => o.Name).ToList();
                    ViewBag.Course = courses;

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
            catch (Exception e)
            {
                TempData["info"] = "error while saving the record";
            }
            return RedirectToAction("List");
        }

        [Authorize]
        public IActionResult DownloadFile(string Id)
        {
            var memory = FilePath(Id, "wwwroot//books");
            return File(memory.ToArray(), "application/pdf", Id);
        }

        [Authorize]
        private MemoryStream FilePath(string fileName, string uploadFolder)
        {
            string videoPath = Path.Combine(uploadFolder, fileName);

            var path = Path.Combine(Directory.GetCurrentDirectory(), videoPath);
            var memeory = new MemoryStream();

            if (System.IO.File.Exists(path))
            {
                var net = new System.Net.WebClient();
                var data = net.DownloadData(path);
                var content = new System.IO.MemoryStream(data);
                memeory = content;
            }
            memeory.Position = 0;
            return memeory;
        }

        [Authorize]
        public IActionResult ReadPDF(string Id)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\books", Id);
            var pdfText = _pdfFileReader.ReadPDF(path);

            return Content(pdfText);
        }


        [Authorize]
        public IActionResult List()
        {
            IList<BookViewModel> bookList = (from book in _dbContext.Books
                                             join course in _dbContext.Courses
                                             on book.CourseId equals course.Id
                                             join batch in _dbContext.Batches
                                             on book.BatchId equals batch.Id
                                             
                                             where book.CourseId == course.Id

                                             select new BookViewModel
                                             {
                                                 Id = book.Id,
                                                 Name = book.Name,
                                                 Description = book.Description,
                                                 CourseId = course.Name,
                                                 BatchId = batch.Name,
                                                 BookURL = book.URL,
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

            var batches = (from batch in _dbContext.Batches
                           join course in _dbContext.Courses
                           on batch.CourseId equals course.Id

                           select new BatchViewModel
                           {
                               Id = batch.Id,
                               Name = batch.Name + "/ " + course.Name
                           }).OrderBy(o => o.Name).ToList();
            ViewBag.Batch = batches;

            return View(editBookData);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(BookViewModel ui)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Define the path to the uploads folder
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "books");

                    // Ensure the directory exists
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Generate a unique file name
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + ui.File.FileName;

                    // Define the full path to the file
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Save the uploaded file to the specified location
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ui.File.CopyToAsync(fileStream);
                    }

                    BookEntity updateBookData = new BookEntity()
                    {
                        Id = ui.Id,
                        CreatedAt = DateTime.UtcNow,
                        ModifiedAt = DateTime.UtcNow,
                        IsInActive = true,
                        Name = ui.Name,
                        Description = ui.Description,
                        URL = uniqueFileName,
                        CourseId = ui.CourseId,
                        BatchId = ui.BatchId,
                    };
                    _dbContext.Books.Update(updateBookData);
                    _dbContext.SaveChanges();
                    TempData["info"] = "update successfully the record";
                }
                else
                {

                    var courses = _dbContext.Courses.Select(s => new CourseViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                    }).OrderBy(o => o.Name).ToList();
                    ViewBag.Course = courses;

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
            catch (Exception e)
            {
                TempData["info"] = "error while updating the record";
            }
            return RedirectToAction("List");
        }
    }
}
