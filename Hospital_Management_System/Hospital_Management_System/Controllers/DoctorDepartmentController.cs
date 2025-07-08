using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class DoctorDepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View("DoctorDepartmentAddEdit");
        }

        public IActionResult DoctorDepartmentList()
        {
            return View();
        }
    }
}
