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

        //Show the list with CRUD operations
        public IActionResult Index()
        {            
            return View(_context.Contries.ToList());
        }

        public IActionResult Create() 
        {
            CountryViewModel myCountryViewModel = new CountryViewModel();

            return View(myCountryViewModel);
        }

        [HttpPost]
        public IActionResult Create(CountryViewModel myCountryViewModel) 
        {
            if (ModelState.IsValid) 
            {
                //Create a new Country
                Country aNewCountry = new Country 
                {
                    CountryName = myCountryViewModel.CountryName
                };

                _context.Contries.Add(aNewCountry);
                _context.SaveChanges();
                //Display the updated view
                return RedirectToAction("Index");
            }

            //Redirect and enter again
            return View(myCountryViewModel);
        }
           

        public IActionResult Edit(int id)
        {
            var countryToEdit = _context.Contries.FirstOrDefault(prospectCountry => prospectCountry.Id == id);

            CountryViewModel myCountryViewModel = new CountryViewModel();
            
            myCountryViewModel.Id = id;
            myCountryViewModel.CountryName = countryToEdit.CountryName;

            ViewBag.NameOfCountryToEdit = countryToEdit.CountryName;
            return View(myCountryViewModel);
        }



        [HttpPost]
        public IActionResult Edit(CountryViewModel myCountryViewModel)
        {
            if (ModelState.IsValid) 
            {
                //Edit the country
                Country editACountry = new Country 
                {
                    Id = myCountryViewModel.Id,
                    CountryName = myCountryViewModel.CountryName
                };

                _context.Update(editACountry);
                _context.SaveChanges();
                //Display updated view
                return RedirectToAction("Index");
            }

            //Redirect to form with error validation
            return View(myCountryViewModel);
        }


        public IActionResult Delete(int id) 
        {
            //Find the item
            var countryToDelete = _context.Contries.FirstOrDefault(prospectCountry => prospectCountry.Id == id);

            //Remove the item
            _context.Contries.Remove(countryToDelete);
            _context.SaveChanges();

            ViewBag.StatusCountryDeleted = $"Success - Delete Country - The Country ' {countryToDelete.CountryName} ' was deleted!";
            return View("Index", _context.Contries.ToList());
        }


        public IActionResult AddACountryToDB() 
        {
            return View();
        }



    }
}
