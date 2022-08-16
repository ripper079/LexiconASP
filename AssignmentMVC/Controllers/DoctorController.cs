using Microsoft.AspNetCore.Mvc;

namespace AssignmentMVC.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FeverCheck()
        {

            ViewBag.danielvalue = 67.33;

            return View();

        }

        [HttpPost]
        public IActionResult FeverCheck(double temperatureunit)
        {
            ViewBag.danielvalue = temperatureunit;

            return View();

        }
    }
}
