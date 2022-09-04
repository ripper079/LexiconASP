using Microsoft.AspNetCore.Mvc;
using AssignmentMVC.Models;
using AssignmentMVC.Data;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AssignmentMVC.Controllers
{
    public class CityController : Controller
    {

        /*private*/
        readonly ApplicationDbContext _context; //skapar en readonly av DbContext

        public CityController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Retreives Cities data from DB
        public IActionResult RetrieveCitiesFromDB()
        {
            //Create a viewModel based on DB content            
            //Create Cities from DB - A list
            var listOfCitiesFromDB = _context.Cities.ToList();

            //Create a city view model
            var myCityViewModel = new CityViewModel();

            //Set view model to that list
            myCityViewModel.listOfCities = listOfCitiesFromDB;

            return View(myCityViewModel);

            //return View();
        }

        public IActionResult AddACityToDB() 
        {
            ViewBag.Countries = new SelectList(_context.Contries, "Id", "CountryName");

            return View();
        }

        [HttpPost]
        public IActionResult AddACityToDB(string NameOfCity, int IdOfCountry)
        {
            if (ModelState.IsValid)
            {
                
                //Make a check that a city already exists in DB - City objects
                var listOfCitiesFromDB = _context.Cities.ToList();
                List<string> allPresentCityNames = new List<string>();

                //Populate all city names
                foreach(var aCity in listOfCitiesFromDB) 
                {
                    allPresentCityNames.Add(aCity.CityName);
                }

                //Doesnt contain the city
                if (!allPresentCityNames.Contains(NameOfCity))
                {
                    //Create a City to DB
                    var userCreateACity = new City { CityName = NameOfCity, Country_Id = IdOfCountry };

                    _context.Cities.Add(userCreateACity);
                    _context.SaveChanges();
                    ViewBag.StatusNewCity = $"Success - Add City - The City '{NameOfCity}' was added";
                }
                else
                {
                    ViewBag.StatusNewCity = $"Failure - Add City - The City '{NameOfCity}' already exists";
                }                
            }
            else 
            {
                ViewBag.StatusNewCity = $"Error: Missing/Invalid input in 'City form'";
            }


            //I may change the the view to accept a IEnumerable insteed, but not sure if i used a viewmodel in previous exercises

            //Create a country view model
            var myCityViewModel = new CityViewModel();
            //Populate with viewModel with the list
            myCityViewModel.listOfCities = _context.Cities.ToList();


            return View("RetrieveCitiesFromDB", myCityViewModel);
        }
    }
}
