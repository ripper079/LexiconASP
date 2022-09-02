using Microsoft.AspNetCore.Mvc;
using AssignmentMVC.Models;
using AssignmentMVC.Data;

//using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AssignmentMVC.Controllers
{
    public class LanguageController : Controller
    {
        readonly ApplicationDbContext _context;

        public LanguageController(ApplicationDbContext context)
        {
            _context = context;
        }



        public IActionResult Index()
        {
            return View();
        }


        public IActionResult RetrieveLanguageDB() 
        {
            //Create List from DB
            var listOfLanguagesFromDB = _context.Languages.ToList();

            return View(listOfLanguagesFromDB);

            //return NotFound("Custom Simulated 404 Not Found Page - In [LanguageController] on [RetrieveLanguageDB()] action");
        }

        public IActionResult DisplayPeopleLanguages()
        {
            List<Person> people = _context.People.Include(p => p.Languages).ToList();

            //return NotFound("Custom Simulated 404 Not Found Page - In [LanguageController] on [ DisplayPeopleLanguages()] action");
            return View(people);
        }
    }
}
