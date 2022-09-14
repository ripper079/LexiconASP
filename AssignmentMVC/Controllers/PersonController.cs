using Microsoft.AspNetCore.Mvc;
using AssignmentMVC.Models;
using AssignmentMVC.Data;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AssignmentMVC.Controllers
{
    [Authorize]
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

        [Authorize(Roles = "User, Moderator, Admin")]
        public IActionResult Index()
        {
            return View(_context.People.ToList());
        }

        [Authorize(Roles = "User, Moderator, Admin")]
        public IActionResult Create() 
        {
            PersonViewModel myPersonViewModel = new PersonViewModel();

            //Limit the option to countries in DB
            ViewBag.Cities = new SelectList(_context.Cities, "Id", "CityName");
            return View(myPersonViewModel);
        }

        [Authorize(Roles = "User, Moderator, Admin")]
        [HttpPost]
        public IActionResult Create(PersonViewModel myPersonViewModel) 
        {
            if (ModelState.IsValid) 
            {
                //Create a new person
                Person aNewPerson = new Person
                {
                    FullName = myPersonViewModel.FullName,
                    PhoneNumber = myPersonViewModel.PhoneNumber,
                    City_Id = myPersonViewModel.City_Id                    
                };

                _context.People.Add(aNewPerson);
                _context.SaveChanges();
                //Display updated view
                return RedirectToAction("Index");
            }

            //SelectList gives an error
            ViewBag.Cities = new SelectList(_context.Cities, "Id", "CityName");
            return View(myPersonViewModel);
        }

        [Authorize(Roles = "User, Moderator, Admin")]
        public IActionResult Edit(int id) 
        {
            Person personToEdit = _context.People.FirstOrDefault(aPerson => aPerson.IdPerson == id);

            PersonViewModel myPersonViewModel = new PersonViewModel();

            myPersonViewModel.IdPerson = id;
            myPersonViewModel.FullName = personToEdit.FullName;
            myPersonViewModel.PhoneNumber = personToEdit.PhoneNumber;
            myPersonViewModel.City_Id = personToEdit.City_Id;

            ViewBag.Cities = new SelectList(_context.Cities, "Id", "CityName");
            return View(myPersonViewModel);
        }

        [Authorize(Roles = "User, Moderator, Admin")]
        [HttpPost]
        public IActionResult Edit(PersonViewModel myPersonViewModel) 
        {
            if (ModelState.IsValid) 
            {
                //Edit the person
                //Create a new person
                Person editAPerson = new Person
                {
                    IdPerson = myPersonViewModel.IdPerson,
                    FullName = myPersonViewModel.FullName,
                    PhoneNumber = myPersonViewModel.PhoneNumber,
                    City_Id = myPersonViewModel.City_Id
                };

                _context.Update(editAPerson);
                _context.SaveChanges();
                //Display updated view
                return RedirectToAction("Index");
            }

            //Prevent the SelectList of being empty and crash
            ViewBag.Cities = new SelectList(_context.Cities, "Id", "CityName");
            return View(myPersonViewModel);
        }

        [Authorize(Roles = "Moderator, Admin")]
        public IActionResult Delete(int id) 
        {
            var thePersonToDelete = _context.People.FirstOrDefault(aPerson => aPerson.IdPerson == id);
            
            _context.People.Remove(thePersonToDelete);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        /*
        public IActionResult EditChooseAPerson() 
        {
            ViewBag.Person = new SelectList(_context.People, "IdPerson", "FullName");

            return View();
        }



        
        //Lets user pick a person to edit
        [HttpPost]
        public IActionResult EditChooseAPerson(int IdOfPersonToEdit)
        {

            if (ModelState.IsValid) 
            {
                //Get details about person
                var personToEdit = _context.People.FirstOrDefault(aPeople => aPeople.IdPerson == IdOfPersonToEdit);

                if (personToEdit == null) 
                {
                    ViewBag.StatusEditPerson = $"Failure - Edit Person - Can NOT edit non-existing the person";

                    //Display all people
                    return View("RetrievePeopleDB", _context.People.ToList());
                }
                else
                {
                    ViewBag.PersonToEditInfo = $"The user to '{personToEdit.IdPerson}  {personToEdit.FullName} with PhoneNumber {personToEdit.PhoneNumber}' edit ";
                    RedirectToAction("EditDisplayForm", IdOfPersonToEdit);
                }
            }
            else 
            {
                ViewBag.StatusEditPerson = $"Error: Missing/Invalid input in 'Person edit form'";
            }

            //return View("RetrievePeopleDB", _context.People.ToList());
            RedirectToAction("EditDisplayForm", IdOfPersonToEdit);
        }

        //Enter the new updated (edited) User
        public IActionResult EditDisplayForm(int idOfPersonToEdit) 
        {
            return View();
        }

        //Here the edit get performed in DB
        [HttpPost]
        public IActionResult EditDisplayForm(Person person)
        {
            if (ModelState.IsValid)
            {

            }
            else
            {

            }

                return View();
        }

        */

        //Backward compatibilty may break
        [Authorize(Roles = "Admin")]
        public IActionResult RemovePersonFromDB() 
        {
            ViewBag.Person = new SelectList(_context.People, "IdPerson", "FullName");

            return View();
        }

        //Backward compatibilty may break
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult RemovePersonFromDB(int IdOfPerson)
        {
            if (ModelState.IsValid) 
            {
                var personToDelete = _context.People.FirstOrDefault(aPeople => aPeople.IdPerson == IdOfPerson);
                if (personToDelete == null)
                {
                    ViewBag.StatusDeletePerson = $"Failure - Delete Person - Can NOT delete non-existing the person";
                }
                else 
                {
                    ViewBag.StatusDeletePerson = $"Success - Delete Person - The Person '{personToDelete.FullName}' was deleted";
                    _context.People.Remove(personToDelete);
                    _context.SaveChanges();
                }
            }
            else
            {
                ViewBag.StatusDeletePerson = $"Error: Missing/Invalid input in 'Person delete form'";
            }


            //Create List from DB
            var listOfPeopleFromDB = _context.People.ToList();

            return View("RetrievePeopleDB", listOfPeopleFromDB);

        }

        //Adds a person to DB
        public IActionResult AddPersonToDB() 
        {
            ViewBag.Cities = new SelectList(_context.Cities, "Id", "CityName");

            return View();
        }

        [HttpPost]
        //Adds a person to DB
        public IActionResult AddPersonToDB(string FullNameOfPerson, string PhoneNumberOfPerson, int IdOfCity)
        {

            if (ModelState.IsValid)
            {

                //Make a check that a city already exists in DB - City objects
                var listOfPeopleFromDB = _context.People.ToList();
                List<string> allPresentPersonNames = new List<string>();

                //Populate all person names names
                foreach (var aPerson in listOfPeopleFromDB)
                {
                    allPresentPersonNames.Add(aPerson.FullName);
                }

                //Doesnt contain the person name
                if (! allPresentPersonNames.Contains(FullNameOfPerson))
                {
                    //Create a City to DB
                    var userCreateAPersony = new Person { FullName = FullNameOfPerson, PhoneNumber = PhoneNumberOfPerson, City_Id=IdOfCity};

                    _context.People.Add(userCreateAPersony);
                    _context.SaveChanges();
                    ViewBag.StatusNewPerson = $"Success - Add Person - The Person '{FullNameOfPerson}' was added";
                }
                else
                {
                    ViewBag.StatusNewPerson = $"Failure - Add PErson - The Person '{FullNameOfPerson}' already exists";
                }
            }
            else
            {
                ViewBag.StatusNewPerson = $"Error: Missing/Invalid input in 'Person form'";
            }

            //Create List from DB
            var updatedListOfPeopleFromDB = _context.People.ToList();

            return View("RetrievePeopleDB", updatedListOfPeopleFromDB);
        }

        //Retrieves the data from the DB
        [Authorize(Roles ="User, Moderator, Admin, FreeUser")]
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

        //Backward compatibilty may break
        [Authorize(Roles = "Admin")]
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

        //Backward compatibilty may break
        [Authorize(Roles = "Admin")]
        public IActionResult AddLanguageSkillToPerson() 
        {
            ViewBag.People = new SelectList(_context.People, "IdPerson", "FullName");
            ViewBag.Languages = new SelectList(_context.Languages, "Id", "Name");

            return View();
        }

        //Adds a language to the DB
        //Backward compatibilty may break
        [Authorize(Roles = "Admin")]
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

        //Backward compatibilty may break
        [Authorize(Roles = "Admin")]
        public IActionResult RemovePerson(int id) 
        {
            myPeopleViewModel.removePersonFromList(id);
            return View("Person", myPeopleViewModel);
        }


        //Backward compatibilty may break
        [Authorize(Roles = "Admin")]
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
