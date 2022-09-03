using AssignmentMVC.Data;
using AssignmentMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentMVC.Controllers
{
    public class CountryController : Controller
    {
        /*private*/
        readonly ApplicationDbContext _context; //skapar en readonly av DbContext

        public CountryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Retreives Countries data from DB
        public IActionResult RetrieveCountriesFromDB()
        {
            //Create a viewModel based on DB content            
            //Create Countries from DB
            var listOfCountriesFromDB = _context.Contries.ToList();

            //Create a country view model
            var myCountryViewModel = new CountryViewModel();
            
            //Populate with viewModel with the list
            myCountryViewModel.listOfCountries = listOfCountriesFromDB;

            return View(myCountryViewModel);
        }

        public IActionResult AddACountryToDB() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddACountryToDB(Country country)
        {
            if (ModelState.IsValid)
            {
                //Make a check that a country doesn't already exist(Languages objects)
                var listOfCountriesFromDB = _context.Contries.ToList();
                List<string> allPresentCountryNames = new List<string>();

                //Populate all countryies
                foreach (var aCountry in listOfCountriesFromDB) 
                {
                    allPresentCountryNames.Add(aCountry.CountryName);
                }

                //Doesnt contain the language
                if (! allPresentCountryNames.Contains(country.CountryName))
                {
                    _context.Contries.Add(country);
                    _context.SaveChanges();
                    ViewBag.StatusNewCountry = $"Success - Add Country - The Country '{country.CountryName}' was added";
                }
                else
                {
                    ViewBag.StatusNewCountry = $"Failure - Add Country - The Country '{country.CountryName}' already exists";
                }

            }
            else
            {
                ViewBag.StatusNewCountry = $"Error: Missing/Invalid input in 'Country form'";
            }



            //I may change the the view to accept a IEnumerable insteed, but not sure if i used a viewmodel in previous exercises
          
            //Create a country view model
            var myCountryViewModel = new CountryViewModel();
            //Populate with viewModel with the list
            myCountryViewModel.listOfCountries = _context.Contries.ToList();

                       

            return View("RetrieveCountriesFromDB", myCountryViewModel);
        }
    }
}
