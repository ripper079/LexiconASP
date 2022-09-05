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
            List<Country> listOfCountries = _context.Contries.ToList();

            return View(listOfCountries);
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Country country) 
        {
            if (ModelState.IsValid) 
            {
                //Make a check here that country doesnt already exists, NO duplicates
                var listOfCountriesFromDB = _context.Contries.ToList();
                List<string> allPresentCountryNames = new List<string>();

                //Populate all country names
                foreach (var aCountry in listOfCountriesFromDB)
                {
                    allPresentCountryNames.Add(aCountry.CountryName);
                }

                //Doesnt contain the language
                if ( !allPresentCountryNames.Contains(country.CountryName))
                {
                    _context.Contries.Add(country);
                    _context.SaveChanges();
                    ViewBag.StatusNewCountry = $"Success - Create Country - The Country '{country.CountryName}' was created";
                    return View("Index", _context.Contries.ToList());
                }
                else
                {
                    ViewBag.StatusNewCountry = $"Failure - Create Country - The Country '{country.CountryName}' already exists";
                    return View("Index", _context.Contries.ToList());
                }

                //_context.Contries.Add(country);
                //_context.SaveChanges();
                //ViewBag.StatusNewCountry = $"Success - Create Country - The Country '{country.CountryName}' was created";
                //return View("Index", _context.Contries.ToList());

            }
            else
            {
                ViewBag.StatusNewCountry = $"Error: Missing/Invalid input in 'Country form'";
                return View("Index", _context.Contries.ToList());
            }

            return RedirectToAction("Index");
        }

        //Jonathan går igenom 44:45 detta
        public IActionResult Edit(int id) 
        {
            Country aCountry = _context.Contries.FirstOrDefault(prospectCountry => prospectCountry.Id == id);
            ViewBag.NameOfCountryToEdit = aCountry.CountryName;
            return View(aCountry);
        }

        [HttpPost]
        public IActionResult Edit(Country country) 
        {
            if (ModelState.IsValid) 
            {
                _context.Update(country);
                _context.SaveChanges();
                ViewBag.StatusCountryEdited = $"Success - Edit Country - 'Changed(edited)' country is '{country.CountryName}'";
            }
            else
            {
                ViewBag.StatusCountryEdited = $"Error: Missing/Invalid input in 'Edit Country form'";
            }


            //return RedirectToAction("Index");
            return View("Index", _context.Contries.ToList());
        }

        public IActionResult Delete(int id) 
        {
            //Find the item
            Country countryToDelete = _context.Contries.FirstOrDefault(prospectCountry => prospectCountry.Id == id);

            //Remove the item
            _context.Contries.Remove(countryToDelete);
            _context.SaveChanges();

            ViewBag.StatusCountryDeleted = $"Success - Delete Country - The Country ' {countryToDelete.CountryName} ' was deleted!";
            return View("Index", _context.Contries.ToList());
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
                //Make a check that a country doesn't already exist (Country objects)
                var listOfCountriesFromDB = _context.Contries.ToList();
                List<string> allPresentCountryNames = new List<string>();

                //Populate all countries
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
