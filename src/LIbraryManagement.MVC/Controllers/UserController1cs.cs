using Microsoft.AspNetCore.Mvc;

namespace LIbraryManagement.MVC.Controllers
{
    public class UserController1cs : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }
    }
}
