using Microsoft.AspNetCore.Mvc;
using AssignmentMVC.Models;
using AssignmentMVC.Data;

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
    }
}
