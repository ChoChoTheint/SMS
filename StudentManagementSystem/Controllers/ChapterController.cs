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
        public IActionResult Entry()
        {
            var batches = _dbContext.Batches.Select(s => new BatchViewModel
            {
                Id = s.Id,
                Name = s.Name,
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
        public IActionResult Entry(ChapterViewModel ui)
        {
            try
            {
                ChapterEntity chapterData = new ChapterEntity()
                {
                    Id = Guid.NewGuid().ToString(),
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
            catch (Exception e)
            {
                TempData["info"] = "error while saving the record";
            }
            return RedirectToAction("Lsit");
        }

        public IActionResult List()
        {
            IList<ChapterViewModel> chapterList = (from chapter in _dbContext.Chapters
                                                   join batch in _dbContext.Batches
                                                   on chapter.BatchId equals batch.Id
                                                   join book in _dbContext.Books
                                                   on chapter.BookId equals book.Id
                                                   join video in _dbContext.Videos
                                                   on chapter.VideoId equals video.Id

                                                   select new ChapterViewModel
                                                   {
                                                       Id = chapter.Id,
                                                       Name = chapter.Name,
                                                       Description = chapter.Description,
                                                       BatchInfo = batch.Name,
                                                       BookInfo = book.Name,
                                                       VideoInfo = video.URL,
                                                   }).ToList();
            return View(chapterList);
        }
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

            var batches = _dbContext.Batches.Select(s => new BatchViewModel
            {
                Id = s.Id,
                Name = s.Name,
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
                    BatchId = ui.BatchInfo,
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
