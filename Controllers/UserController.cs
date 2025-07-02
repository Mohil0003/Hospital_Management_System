using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View("UserAddEdit");
        }
        public IActionResult UserList()
        {
            return View();
        }
    }
}
