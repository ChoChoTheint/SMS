using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.DataModels;
using StudentManagementSystem.Models.ViewModels;

namespace StudentManagementSystem.Controllers
{
    public class ChapterController : Controller
    {
        private readonly SMSDbContext _dbContext;
        public ChapterController(SMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        public IActionResult Entry()
        {
<<<<<<< HEAD
=======
            ViewBag.Id = Guid.NewGuid().ToString();
>>>>>>> yairyint

            var batches = (from batch in _dbContext.Batches
                           join course in _dbContext.Courses
                           on batch.CourseId equals course.Id
<<<<<<< HEAD
=======

>>>>>>> yairyint
                           select new BatchViewModel
                           {
                               Id = batch.Id,
                               Name = batch.Name + "/ " + course.Name
                           }).OrderBy(o => o.Name).ToList();
            ViewBag.Batch = batches;

            var books = _dbContext.Books.Select(s => new BookViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Book = books;

            var videos = _dbContext.Videos.Select(s => new VideoViewModel
            {
                Id = s.Id,
                Name = s.Name,
                //URL = s.URL,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Video = videos;


            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Entry(ChapterViewModel ui)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    ChapterEntity chapterData = new ChapterEntity()
                    {
                        Id = ui.Id,
                        CreatedAt = DateTime.UtcNow,
                        IsInActive = true,
                        Name = ui.Name,
                        Description = ui.Description,
                        BatchId = ui.BatchId,
                        BookId = ui.BookId,
                        VideoId = ui.VideoId,
                    };

                    _dbContext.Chapters.Add(chapterData);
                    _dbContext.SaveChanges();
                    TempData["info"] = "save successfully the record";
                }

                if (!ModelState.IsValid)
                {
                    //Relode id, batch, book and video to populate dropdown again!

                    ViewBag.Id = Guid.NewGuid().ToString();

                    var batches = (from batch in _dbContext.Batches
                                   join course in _dbContext.Courses
                                   on batch.CourseId equals course.Id

                                   select new BatchViewModel
                                   {
                                       Id = batch.Id,
                                       Name = batch.Name + "/ " + course.Name
                                   }).OrderBy(o => o.Name).ToList();
                    ViewBag.Batch = batches;

                    var books = _dbContext.Books.Select(s => new BookViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                    }).OrderBy(o => o.Name).ToList();
                    ViewBag.Book = books;

                    var videos = _dbContext.Videos.Select(s => new VideoViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        //URL = s.URL,
                    }).OrderBy(o => o.Name).ToList();
                    ViewBag.Video = videos;

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
        public IActionResult List()
        {
            IList<ChapterViewModel> chapterList = (from chapter in _dbContext.Chapters
                                                   join batch in _dbContext.Batches
                                                   on chapter.BatchId equals batch.Id
                                                   join book in _dbContext.Books
                                                   on chapter.BookId equals book.Id
                                                   join video in _dbContext.Videos
                                                   on chapter.VideoId equals video.Id
                                                   join course in _dbContext.Courses
                                                   on batch.CourseId equals course.Id

                                                   select new ChapterViewModel
                                                   {
                                                       Id = chapter.Id,
                                                       Name = chapter.Name,
                                                       Description = chapter.Description,
                                                       BatchId = batch.Name + "/ " + course.Name,
                                                       BookId = book.Name,
                                                       VideoId = video.URL,
                                                   }).ToList();
            return View(chapterList);
        }

        [Authorize]
        public IActionResult Delete(string Id)
        {
            try
            {
                var deleteChapterData = _dbContext.Chapters.Where(w => w.Id == Id).FirstOrDefault();
                if(deleteChapterData is not null)
                {
                    _dbContext.Chapters.Remove(deleteChapterData);
                    _dbContext.SaveChanges();
                }
                TempData["info"] = "delete successfully the record";            }
            catch (Exception e)
            {
                TempData["info"] = "error while deleting the record";
            }
            return RedirectToAction("List");
        }

        [Authorize]
        public IActionResult Edit(string Id)
        {
            ChapterViewModel editChapterData = _dbContext.Chapters.Where(w => w.Id == Id).Select(s => new ChapterViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                BatchId  = s.BatchId,
                BookId = s.BookId,
                VideoId = s.VideoId,

            }).FirstOrDefault();

            var batches = (from batch in _dbContext.Batches
                           join course in _dbContext.Courses
                           on batch.CourseId equals course.Id
                           select new BatchViewModel
                           {
                               Id = batch.Id,
                               Name = batch.Name+"/ "+course.Name
                           }).OrderBy(o => o.Name).ToList();
            ViewBag.Batch = batches;

            var books = _dbContext.Books.Select(s => new BookViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Book = books;

            var videos = _dbContext.Videos.Select(s => new VideoViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList();
            ViewBag.Video = videos;

            return View(editChapterData);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(ChapterViewModel ui)
        {
            try
            {
                ChapterEntity updateChapterData = new ChapterEntity()
                {
                    Id = ui.Id,
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                    IsInActive = true,
                    Name = ui.Name,
                    Description = ui.Description,
                    BatchId = ui.BatchId,
                    BookId = ui.BookId,
                    VideoId = ui.VideoId,
                };
                _dbContext.Chapters.Update(updateChapterData);
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
