using AssignmentMVC.Data;
using Microsoft.AspNetCore.Mvc;
using AssignmentMVC.Models;
using AssignmentMVC.Data;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AssignmentMVC.Controllers
{
    public class PersonLanguageController : Controller
    {
        /*private*/
        readonly ApplicationDbContext _context; //skapar en readonly av DbContext

        public PersonLanguageController(ApplicationDbContext context)
        {
            _context = context;
        }

        //The R in CRUD
        public IActionResult Index()
        {
            //A list of people with an optional list of languages
            List<Person> peopleLanguageSkills = _context.People.Include(x => x.Languages).ToList();

            return View(peopleLanguageSkills);
        }
        
        //The C in CRUD [id is the id for person]
        //Creates a new language to a person
        public IActionResult Create(int id) 
        {
            ViewBag.Languages = new SelectList(_context.Languages, "Id", "Name");
            
            Person aPersonToAddLanguageSkill = _context.People.FirstOrDefault(aPerson => aPerson.IdPerson == id);

            //?? Maybe a Viewbag will be enough
            return View(aPersonToAddLanguageSkill);
        }



        //Adds and validates that language should be added to person
        [HttpPost]
        public IActionResult Create(int IdOfLanguage, int IdOfPerson) 
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
                                //ViewBag.LanguageStatus = "Failure - The user " + nameOfPersonDuplicateLanguage + " already knows '" + dublicateLanguage + "'";

                                return RedirectToAction("Index");
                            }
                        }
                    }
                }

                //ViewBag.LanguageStatus = "Success - The user " + person.FullName + " has been added the language '" + language.Name + "'";
                //Means that language doesnt exist
                person.Languages.Add(language);
                //Save to db
                _context.SaveChanges();
            }

            return RedirectToAction("Index");

        }

    }
}
