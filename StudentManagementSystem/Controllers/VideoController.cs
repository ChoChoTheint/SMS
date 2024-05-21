using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DAO;

namespace StudentManagementSystem.Controllers
{
    public class VideoController : Controller
    {
        private readonly SMSDbContext _dbContext;
        public VideoController(SMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Entry()
        {
            return View();
        }
    }
}
