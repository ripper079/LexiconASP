using Microsoft.AspNetCore.Mvc;

namespace AssignmentMVC.Controllers
{
    public class CountryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
