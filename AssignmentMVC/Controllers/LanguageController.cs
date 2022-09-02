using Microsoft.AspNetCore.Mvc;
using AssignmentMVC.Models;
using AssignmentMVC.Data;

using Microsoft.AspNetCore.Mvc.Rendering;
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
        }

        public IActionResult DisplayPeopleLanguages()
        {
            List<Person> people = _context.People.Include(x => x.Languages).ToList();            

            return View(people);
        }

        public IActionResult AddLanguage()
        {
            ViewBag.People = new SelectList(_context.People, "IdPerson", "FullName");
            ViewBag.Languages = new SelectList(_context.Languages, "Id", "Name");


            return View();
        }

        //Adds a language to the DB
        [HttpPost]
        public IActionResult AddLanguage(int IdOfPerson, int IdOfLanguage) 
        {
            List<Person> people = _context.People.Include(x => x.Languages).ToList();

            if (ModelState.IsValid) 
            {
                //Get the first occurrence of person/language
                var person = _context.People.FirstOrDefault(aPerson => aPerson.IdPerson == IdOfPerson);
                var language = _context.Languages.FirstOrDefault(aLanguage => aLanguage.Id == IdOfLanguage);

                //Prevents duplicate
                foreach (var aPerson in people) 
                {                    
                    if (aPerson.IdPerson == IdOfPerson)
                    {
                        foreach (var aLanguage in aPerson.Languages) 
                        {
                            //If that language exist show 
                            if (aLanguage.Id == IdOfLanguage)
                            {
                                var dublicateLanguage = aLanguage.Name;
                                var nameOfPersonDuplicateLanguage = aPerson.FullName;
                                ViewBag.LanguageStatus = "Failure - The user " + nameOfPersonDuplicateLanguage + " already knows '" + dublicateLanguage + "'";

                                return View("DisplayPeopleLanguages", people);
                            }
                        }
                    }
                }

                ViewBag.LanguageStatus = "Success - The user " + person.FullName + " has been added the language '" + language.Name + "'";
                //Means that language doesnt exist
                person.Languages.Add(language);
                //Save to db
                _context.SaveChanges();
            }

            return View("DisplayPeopleLanguages", people);
        }


    }
}
