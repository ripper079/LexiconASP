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
    }
}
