using Microsoft.AspNetCore.Mvc;
using AssignmentMVC.Models;

namespace AssignmentMVC.Controllers
{
    public class PersonController : Controller
    {        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Person()
        {
            PeopleViewModel myPeopleView = new PeopleViewModel();

            return View(myPeopleView);           
        }

        [HttpPost]
        public IActionResult Person(PeopleViewModel model)
        {
            model.City

            return View();
        }
    }
}
