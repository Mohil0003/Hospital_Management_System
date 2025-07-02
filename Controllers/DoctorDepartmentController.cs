using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class DoctorDepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View("DoctorDeparmentAddEdit");
        }

        public IActionResult DoctorDeparmentList()
        {
            return View();
        }
    }
}
