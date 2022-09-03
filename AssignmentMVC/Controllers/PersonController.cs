using Microsoft.AspNetCore.Mvc;
using AssignmentMVC.Models;
using AssignmentMVC.Data;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AssignmentMVC.Controllers
{
    public class PersonController : Controller
    {        
        public static PeopleViewModel myPeopleViewModel = new PeopleViewModel();
        public static int IDForPeople = 100;        //Destinguish between default id and ids added later in myPeopleViewModel

        /*private*/ readonly ApplicationDbContext _context; //skapar en readonly av DbContext

        //Magic in action
        //Dependecy injection - Solves the problem that the programmer cant directly call the constructor - Do NOT forget to finish config in program.cs
        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return NotFound("Custom Simulated 404 Not Found Page - In [PersonController] on [Index()] action");     //"Custom page"

            //return NotFound();                                                                              //"Standard" custom - Returns a HTTP ERROR 404
            //return View();
        }


        //Retrieves the data from the DB
        public IActionResult RetrievePeopleDB()
        {
          
            //Create List from DB
            var listOfPeopleFromDB = _context.People.ToList();                  

            return View(listOfPeopleFromDB);
        }

        public IActionResult DisplayPeopleLanguages()
        {
            List<Person> people = _context.People.Include(x => x.Languages).ToList();

            return View(people);
        }


        public IActionResult LINQSearchPeople()
        {
            //Reference
            // https://docs.microsoft.com/en-us/dotnet/csharp/linq/write-linq-queries
            string filterFullName = "Vladimir Putin";

            //Create List from DB
            var listOfPeopleFromDB = _context.People.ToList();

            //Filtered the List (Using Linq), resuing
            var filteredPeople = listOfPeopleFromDB.
                                    Where(aPeople => aPeople.FullName == filterFullName).
                                    ToList();

            //Previous statement is equivalent to
            //IEnumerable<Person> filteringQuery =
            //    from aRow in listOfPeopleFromDB
            //    where aRow.FullName == filterFullName 
            //    select aRow;
            //var filteredPeople = filteringQuery.ToList();

            //Create a People Model
            //Create a new filtered viewmodel
            //var filteredViewModel = new PeopleViewModel();
            //Set the filtered view 
            //filteredViewModel.listOfPersons = filteredPeople;


            return View(filteredPeople);
        }
      
 
        [HttpGet]
        public IActionResult Person()
        {
            return View(myPeopleViewModel);           
        }


        public IActionResult AddLanguageSkillToPerson() 
        {
            ViewBag.People = new SelectList(_context.People, "IdPerson", "FullName");
            ViewBag.Languages = new SelectList(_context.Languages, "Id", "Name");

            return View();
        }

        //Adds a language to the DB
        [HttpPost]
        public IActionResult AddLanguageSkillToPerson(int IdOfPerson, int IdOfLanguage)
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


        //Adds (if possible) a person
        [HttpPost]
        public IActionResult Person(PeopleViewModel pPeopleViewModel)
        {
            if (ModelState.IsValid) 
            {
                myPeopleViewModel.addPersonToList(
                    pPeopleViewModel.cpvm.FullName, pPeopleViewModel.cpvm.PhoneNumber, pPeopleViewModel.cpvm.City, IDForPeople++
                );
            }
            
            return View(myPeopleViewModel);
        }
       

        public IActionResult RemovePerson(int id) 
        {
            myPeopleViewModel.removePersonFromList(id);
            return View("Person", myPeopleViewModel);
        }


        [HttpPost]
        public IActionResult Filter(string filtertext) 
        {
            //Restore original viewmodel
            if (string.IsNullOrEmpty(filtertext))
            {
                return View("Person", myPeopleViewModel);
            }
            
            return View("Person", CreateFilteredViewModel(filtertext));
        }


        //Creates a new Filtered View Model base in filterText
        private PeopleViewModel CreateFilteredViewModel(string filterText) 
        {
            //Create a filtered list based original viewmodel
            var filteredPeople = myPeopleViewModel.listOfPersons.Where
                (aPeople => aPeople.FullName == filterText || aPeople.CityOfPerson.CityName == filterText).ToList();

            //Create a new filtered viewmodel
            var filteredViewModel = new PeopleViewModel();
            //Set the filtered view 
            filteredViewModel.listOfPersons = filteredPeople;

            return filteredViewModel;
        }



        //[HttpGet]
        //public IActionResult AddPerson()
        //{
        //    return View();
        //}


        //[HttpPost]
        //public IActionResult AddPerson(Person Create)
        //{
        //    Console.WriteLine(Create);
        //    return View();
        //}




    }
}
