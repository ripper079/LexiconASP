using Microsoft.AspNetCore.Mvc;

namespace AssignmentMVC.Controllers
{
    public class AjaxController : Controller
    {
        public IActionResult Index()
        {
            return NotFound("Custom Simulated 404 Not Found Page - In [AjaxController] on [Index()] action");     //"Custom page"
            //return View();
        }
    }
}
