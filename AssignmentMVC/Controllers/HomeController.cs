using Microsoft.AspNetCore.Mvc;
using AssignmentMVC.Models;

namespace AssignmentMVC.Controllers
{
    

    public class HomeController : Controller
    {

        

        public IActionResult Index()
        {
            return View();
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

        public IActionResult GuessingGame()
        {
            //Means that no entry exist - First time it hits and expiration of session is NOT fullfilled
            //if (HttpContext.Session.GetString("sessionSecretNumber") == null)
            //if (String.IsNullOrEmpty(HttpContext.Session.GetString("sessionSecretNumber")))
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("activesession")))
            {
                //The secret number generated 1 to 100
                int randomNumberGeneratedToGuess = Utilities.GenerateSecretNumber();
                int countUserGuesses = 0;

                //Set/store the secret number as a string in session
                //HttpContext.Session.SetString("sessionSecretNumber", randomNumberGeneratedToGuess.ToString());
                //HttpContext.Session.SetString("sessionCountUserGuesses", "0");

                HttpContext.Session.SetInt32("sessionSecretNumber", randomNumberGeneratedToGuess);
                HttpContext.Session.SetInt32("sessionCountUserGuesses", countUserGuesses);

                //Acts as flag
                HttpContext.Session.SetString("activesession", "yes");


                //Make secret number availible to view
                ViewBag.secretNumber = randomNumberGeneratedToGuess;
                ViewBag.countUserGuesess = countUserGuesses;
            }

            return View();
        }

        [HttpPost]
        public IActionResult GuessingGame(int? guessednumber)
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

            ////Get the secret number from the session
            //String stringNumberfromSession = HttpContext.Session.GetString("sessionSecretNumber");
            ////And parse it correctly
            //int intNumberFromSession;
            //Int32.TryParse(stringNumberfromSession, out intNumberFromSession);

            ////Get counts of user guesses
            //String stringCountUserGuesses = HttpContext.Session.GetString("sessionCountUserGuesses");
            ////Parse count user guesses
            //int countUserGuesses;
            //Int32.TryParse(stringCountUserGuesses, out countUserGuesses);
            ////Increase guess count
            //countUserGuesses++;
            ////Set new value for sessionCountUserGuesses
            //HttpContext.Session.SetString("sessionSecretNumber", countUserGuesses.ToString());

            ////The secret number to be guessed
            //ViewBag.secretNumber = intNumberFromSession;

            ////Count user guesses
            //ViewBag.countUserGuesess = countUserGuesses;

            ////The number that the user guessed - From Form in view
            //ViewBag.userGuessedNumberFromForm = guessednumber;

            //Message to indicate to low/high user guess
            //if (guessednumber > intNumberFromSession) 
            //{
            //    ViewBag.messageToLowOrToHigh = " is to high";
            //}
            //else if (guessednumber < intNumberFromSession)
            //{
            //    ViewBag.messageToLowOrToHigh = " is to low";
            //}
            //else 
            //{
            //    ViewBag.messageToLowOrToHigh = " is CORRECT";
            //}

            return View();
        }
    }
}
