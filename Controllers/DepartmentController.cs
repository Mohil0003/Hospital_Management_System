using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View("DepartmentAddEdit");
        }

        public IActionResult DepartmentList()
        {
            return View();
        }


    }
}
