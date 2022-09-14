using Microsoft.AspNetCore.Mvc;
using AssignmentMVC.Models;
using AssignmentMVC.Data;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AssignmentMVC.Controllers
{
    [Authorize(Roles ="Admin")]
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
            return View(_context.Cities.ToList());
        }

        public IActionResult Create()
        {
            CityViewModel myCityViewModel = new CityViewModel();


            //Limit the option to countries in DB
            ViewBag.Countries = new SelectList(_context.Contries, "Id", "CountryName");
            return View(myCityViewModel);
        }

        [HttpPost]
        public IActionResult Create(CityViewModel myCityViewModel) 
        {
            if (ModelState.IsValid) 
            {
                //Create a new City
                City aNewCity = new City 
                {
                    CityName = myCityViewModel.CityName,
                    Country_Id = myCityViewModel.Country_Id
                };

                _context.Cities.Add(aNewCity);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }

            //Prevent empty field when returning back
            ViewBag.Countries = new SelectList(_context.Contries, "Id", "CountryName");
            return View(myCityViewModel);
        }

        public IActionResult Edit(int id)
        {
            City cityToEdit = _context.Cities.FirstOrDefault(prospectCountry => prospectCountry.Id == id);

            CityViewModel myCityViewModel = new CityViewModel();

            myCityViewModel.Id = id;
            myCityViewModel.CityName = cityToEdit.CityName;
            myCityViewModel.Country_Id = cityToEdit.Country_Id;


            ViewBag.NameOfCityToEdit = cityToEdit.CityName;
            ViewBag.Countries = new SelectList(_context.Contries, "Id", "CountryName");

            return View(myCityViewModel);
        }

        [HttpPost]
        public IActionResult Edit(CityViewModel myCityViewModel) 
        {
            if (ModelState.IsValid) 
            {
                //Edit the city
                City editACity = new City 
                {
                    Id = myCityViewModel.Id,
                    CityName = myCityViewModel.CityName,
                    Country_Id = myCityViewModel.Country_Id
                };

                _context.Update(editACity);
                _context.SaveChanges();
                //Display updated view
                return RedirectToAction("Index");
            }

            //Enter again valid data
            ViewBag.Countries = new SelectList(_context.Contries, "Id", "CountryName");
            return View(myCityViewModel);
        }


        //Hmmm ModelState.IsValid returns false when I try to bind to a model
        //[HttpPost]
        //public IActionResult Create(City city)
        //{
        //    //Hack solution in my opinion
        //    ModelState.Remove("Country");

        //    if (ModelState.IsValid)
        //    {
        //        //Add the city to table
        //        _context.Cities.Add(city);
        //        _context.SaveChanges();
        //        ViewBag.StatusNewCity = $"Success - Add City - The City '{city.CityName}' was added";
        //    }

        //    return View("Index", _context.Cities.ToList());
        //}




        //[HttpPost]
        //public IActionResult Create(string NameOfCity, int IdOfCountry)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //Make a check that a city already exists in DB - City objects
        //        var listOfCitiesFromDB = _context.Cities.ToList();
        //        List<string> allPresentCityNames = new List<string>();

        //        //Populate all city names
        //        foreach (var aCity in listOfCitiesFromDB)
        //        {
        //            allPresentCityNames.Add(aCity.CityName);
        //        }

        //        //Doesnt contain the city
        //        if (!allPresentCityNames.Contains(NameOfCity))
        //        {
        //            //Create a City to DB
        //            var userCreateACity = new City { CityName = NameOfCity, Country_Id = IdOfCountry };

        //            _context.Cities.Add(userCreateACity);
        //            _context.SaveChanges();
        //            ViewBag.StatusNewCity = $"Success - Add City - The City '{NameOfCity}' was added";
        //        }
        //        else
        //        {
        //            ViewBag.StatusNewCity = $"Failure - Add City - The City '{NameOfCity}' already exists";
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.StatusNewCity = $"Error: Missing/Invalid input in 'City form'";
        //    }

        //    return View("Index", _context.Cities.ToList());
        //}





        ////Displays the Form for edit
        //public IActionResult Edit(int id)
        //{
        //    City aCity = _context.Cities.FirstOrDefault(prospectCountry => prospectCountry.Id == id);            

        //    ViewBag.NameOfCityToEdit = aCity.CityName;

        //    ViewBag.Countries = new SelectList(_context.Contries, "Id", "CountryName");
        //    return View(aCity);
        //}


        ////Make the actual change
        //[HttpPost]
        //public IActionResult Edit(City city)
        //{
        //    ModelState.Remove("Country");
        //    if (ModelState.IsValid) 
        //    {
        //        _context.Update(city);
        //        _context.SaveChanges();
        //    }

        //        return View("Index", _context.Cities.ToList());
        //}



        public IActionResult Delete(int id) 
        {
            City theCityToDelete = _context.Cities.FirstOrDefault(aCity => aCity.Id == id);

            ViewBag.StatusDeletedCity = $"Success - Delete City - The City '{theCityToDelete.CityName}' was Deleted";
            _context.Cities.Remove(theCityToDelete);
            _context.SaveChanges();


            //return View("Index", _context.Cities.ToList());
            return RedirectToAction("Index");
        }



        //Retreives Cities data from DB //Obsoltee

        //public IActionResult RetrieveCitiesFromDB()
        //{
        //    Create a viewModel based on DB content
        //    Create Cities from DB - A list
        //    var listOfCitiesFromDB = _context.Cities.ToList();

        //    Create a city view model
        //    var myCityViewModel = new CityViewModel();

        //    Set view model to that list
        //    myCityViewModel.listOfCities = listOfCitiesFromDB;

        //    return View(myCityViewModel);

        //    return View();
        //}




        //public IActionResult AddACityToDB() 
        //{
        //    ViewBag.Countries = new SelectList(_context.Contries, "Id", "CountryName");

        //    return View();
        //}

        //[HttpPost]
        //public IActionResult AddACityToDB(string NameOfCity, int IdOfCountry)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        //Make a check that a city already exists in DB - City objects
        //        var listOfCitiesFromDB = _context.Cities.ToList();
        //        List<string> allPresentCityNames = new List<string>();

        //        //Populate all city names
        //        foreach(var aCity in listOfCitiesFromDB) 
        //        {
        //            allPresentCityNames.Add(aCity.CityName);
        //        }

        //        //Doesnt contain the city
        //        if (!allPresentCityNames.Contains(NameOfCity))
        //        {
        //            //Create a City to DB
        //            var userCreateACity = new City { CityName = NameOfCity, Country_Id = IdOfCountry };

        //            _context.Cities.Add(userCreateACity);
        //            _context.SaveChanges();
        //            ViewBag.StatusNewCity = $"Success - Add City - The City '{NameOfCity}' was added";
        //        }
        //        else
        //        {
        //            ViewBag.StatusNewCity = $"Failure - Add City - The City '{NameOfCity}' already exists";
        //        }                
        //    }
        //    else 
        //    {
        //        ViewBag.StatusNewCity = $"Error: Missing/Invalid input in 'City form'";
        //    }


        //    //I may change the the view to accept a IEnumerable insteed, but not sure if i used a viewmodel in previous exercises

        //    //Create a country view model
        //    var myCityViewModel = new CityViewModel();
        //    //Populate with viewModel with the list
        //    myCityViewModel.listOfCities = _context.Cities.ToList();


        //    return View("RetrieveCitiesFromDB", myCityViewModel);
        //}




    }
}
