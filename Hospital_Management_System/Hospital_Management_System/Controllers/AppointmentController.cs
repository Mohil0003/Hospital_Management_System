using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View("AppointmentAddEdit");
        }

        public IActionResult AppointmentList()
        {
            return View();
        }
    }
}
