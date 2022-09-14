using AssignmentMVC.Data;
using Microsoft.AspNetCore.Mvc;
using AssignmentMVC.Models;
using AssignmentMVC.Data;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AssignmentMVC.Controllers
{
    [Authorize]
    public class PersonLanguageController : Controller
    {
        /*private*/
        readonly ApplicationDbContext _context; //skapar en readonly av DbContext

        public PersonLanguageController(ApplicationDbContext context)
        {
            _context = context;
        }

        //The R in CRUD
        [Authorize(Roles = "User, Moderator, Admin")]
        public IActionResult Index()
        {
            //A list of people with an optional list of languages
            List<Person> peopleLanguageSkills = _context.People.Include(x => x.Languages).ToList();

            return View(peopleLanguageSkills);
        }

        //The C in CRUD [id is the id for person]
        //Creates a new language to a person
        [Authorize(Roles = "User, Moderator, Admin")]
        public IActionResult Create(int id) 
        {                        
            Person aPersonToAddLanguageSkill = _context.People.FirstOrDefault(aPerson => aPerson.IdPerson == id);

            ViewBag.Languages = new SelectList(_context.Languages, "Id", "Name");
            return View(aPersonToAddLanguageSkill);  //?? Maybe a Viewbag will be enough 
        }



        //Adds and validates that language should be added to person
        [HttpPost]
        [Authorize(Roles = "User, Moderator, Admin")]
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

        //The D in CRUD [id is the id for person]
        //
        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult Delete(int id) 
        {
            //Get the Person
            Person aPersonsLanguageSkillDelete = _context.People.FirstOrDefault(aPerson => aPerson.IdPerson == id);
            //Join the tables to populate langues for aPersonsLanguageSkillDelete
            List<Person> people = _context.People.Include(aPersonsLanguageSkillDelete => aPersonsLanguageSkillDelete.Languages).ToList();

            //The person all know languages
            var allKnowLanguages = aPersonsLanguageSkillDelete.Languages.ToList();

            //Limit languages option to persons actual language skills
            ViewBag.Languages = new SelectList(allKnowLanguages, "Id", "Name");

            //Illiterate
            if (aPersonsLanguageSkillDelete.Languages.Count == 0)
            {
                return RedirectToAction("Index");
            }   

            return View(aPersonsLanguageSkillDelete);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult Delete(int IdOfLanguage, int IdOfPerson) 
        {            
            Person myPerson = _context.People.Single(aPerson => aPerson.IdPerson == IdOfPerson);
            Language myLanguage = _context.Languages.Include(langP => langP.People).Single(langI => langI.Id == IdOfLanguage);            

            Console.WriteLine("After remove but not store");

            myLanguage.People.Remove(myLanguage.People.Where(prospectPerson => prospectPerson.IdPerson == myPerson.IdPerson).FirstOrDefault());
            _context.SaveChanges();

            
            return RedirectToAction("Index");
        }

    }
}
