using Microsoft.AspNetCore.Mvc;
using AssignmentMVC.Models;
using AssignmentMVC.Data;

namespace AssignmentMVC.Controllers
{

    public class HomeController : Controller
    {        
        /*private*/ readonly ApplicationDbContext _context; //skapar en readonly av DbContext
        
        //Magic in action
        //Dependecy injection - Solves the problem that the programmer cant directly call the constructor - Do NOT forget to finish config in program.cs
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Main Page
        public IActionResult Index()
        {
            return View();

        }

        //Retrieves the data from the DB
        public IActionResult RetrieveData() 
        {
            return View(_context);
        }
        
        public IActionResult LINQSearchPeople() 
        {
            //Refernce
            // https://docs.microsoft.com/en-us/dotnet/csharp/linq/write-linq-queries
            string filterTextCity = "Moskva";
            string filterFullName = "Vladimir Putin";

            //Create List from DB
            var listOfPeopleFromDB = _context.People.ToList();

            //Filtered the List (Using Linq), resuing
            var filteredPeople = listOfPeopleFromDB.
                                    Where(aPeople => aPeople.FullName == filterFullName || aPeople.City == filterTextCity).
                                    ToList();


            //Previous statement is equivalent to
            //IEnumerable<Person> filteringQuery =
            //    from aRow in listOfPeopleFromDB
            //    where aRow.FullName == filterFullName || aRow.City == filterTextCity
            //    select aRow;
            //var filteredPeople = filteringQuery.ToList();
            


            //Create a People Model
            //Create a new filtered viewmodel
            var filteredViewModel = new PeopleViewModel();
            //Set the filtered view 
            filteredViewModel.listOfPersons = filteredPeople;


            return View("_ListPersonsWithoutId", filteredViewModel);

            
        }

        public IActionResult Test()
        {
            return View();
        }

        public IActionResult Aboutmeperson()
        {
            return View();
        }

        public IActionResult ContactMe()
        {
            return View();
        }

        public IActionResult MyProjects()
        {
            return View();
        }


        //Changed from public
        private void InitSessionValues() 
        {
            //The secret number generated 1 to 100
            int randomNumberGeneratedToGuess = Utilities.GenerateSecretNumber();
            int countUserGuesses = 0;

            HttpContext.Session.SetInt32("sessionSecretNumber", randomNumberGeneratedToGuess);
            HttpContext.Session.SetInt32("sessionCountUserGuesses", countUserGuesses);

            //Acts as flag
            HttpContext.Session.SetString("activeGame", "yes");


            //Make secret number availible to view
            ViewBag.secretNumber = randomNumberGeneratedToGuess;
            ViewBag.countUserGuesess = countUserGuesses;
        }

        public IActionResult GuessingGame()
        {
            //Means that no entry exist - First time it hits and expiration of session is NOT fullfilled            
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("activeGame")))
            {
                InitSessionValues();
            }

            return View();
        }

        [HttpPost]
        public IActionResult GuessingGame(int? guessednumber)
        {

            if (String.IsNullOrEmpty(HttpContext.Session.GetString("activeGame")))
            {
                InitSessionValues();
            }

            if (guessednumber != null)
            {
                //The number that the player should try to guess
                int? secretNumber = HttpContext.Session.GetInt32("sessionSecretNumber");
                ViewBag.secretNumber = secretNumber;

                //How many times the user has guessed
                int? countUserGuesses = HttpContext.Session.GetInt32("sessionCountUserGuesses");
                countUserGuesses++;
                HttpContext.Session.SetInt32("sessionCountUserGuesses", (int)countUserGuesses);
                ViewBag.countUserGuesess = countUserGuesses;

                //The number that the user guessed
                ViewBag.userGuessedNumberFromForm = guessednumber;

                //Message to indicate to low/high user guess
                if (guessednumber > secretNumber)
                {
                    ViewBag.messageToLowOrToHigh = " is to high";
                }
                else if (guessednumber < secretNumber)
                {
                    ViewBag.messageToLowOrToHigh = " is to low";
                }
                else
                {
                    ViewBag.messageToLowOrToHigh = " is CORRECT";
                    //Game over
                    HttpContext.Session.Remove("activeGame");
                }

            }
            return View();
        }
    }
}
