using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystem.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Entry()
        {
            return View();
        }
    }
}
