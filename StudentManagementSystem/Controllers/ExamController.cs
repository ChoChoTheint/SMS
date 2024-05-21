using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystem.Controllers
{
    public class ExamController : Controller
    {
        public IActionResult Entry()
        {
            return View();
        }
    }
}
