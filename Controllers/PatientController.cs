using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View("PatientAddEdit");
        }
        public IActionResult PatientList()
        {
            return View();
        }

    }
}
