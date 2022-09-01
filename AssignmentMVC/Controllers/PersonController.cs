using Microsoft.AspNetCore.Mvc;
using AssignmentMVC.Models;
using AssignmentMVC.Data;

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


        [HttpPost]
        public IActionResult AddPerson(Person Create)
        {
            Console.WriteLine(Create);
            return View();
        }
    }
}
