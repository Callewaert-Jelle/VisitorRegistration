using Microsoft.AspNetCore.Mvc;

namespace VisitorRegistration.Controllers
{
    public class VisitationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            return View();
        }
    }
}
