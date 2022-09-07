using Microsoft.AspNetCore.Mvc;
using AssignmentMVC.Models;
using AssignmentMVC.Data;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AssignmentMVC.Controllers
{
    public class LanguageController : Controller
    {
        readonly ApplicationDbContext _context;

        public LanguageController(ApplicationDbContext context)
        {
            _context = context;
        }



        public IActionResult Index()
        {
            return View(_context.Languages.ToList());
        }

        public IActionResult Create() 
        {
            LanguageViewModel myLanguageViewModel = new LanguageViewModel();

            return View(myLanguageViewModel);
        }

        [HttpPost]
        public IActionResult Create(LanguageViewModel myLanguageViewModel)
        {
            if (ModelState.IsValid) 
            {
                //Create a new language
                Language aNewLanguage = new Language 
                {
                    Name = myLanguageViewModel.Name
                };

                _context.Languages.Add(aNewLanguage);
                _context.SaveChanges();
                //Display updated model
                return RedirectToAction("Index");
            }

            return View(myLanguageViewModel);
        }

        public IActionResult Edit(int id) 
        {
            Language languageToEdit = _context.Languages.FirstOrDefault(aLanguage => aLanguage.Id == id);

            LanguageViewModel myLanguageViewModel = new LanguageViewModel();

            myLanguageViewModel.Id = id;
            myLanguageViewModel.Name = languageToEdit.Name;

            return View(myLanguageViewModel);
        }

        [HttpPost]
        public IActionResult Edit(LanguageViewModel myLanguageViewModel) 
        {
            if (ModelState.IsValid) 
            {
                Language editALanguage = new Language 
                {
                    Id = myLanguageViewModel.Id,
                    Name = myLanguageViewModel.Name
                };

                _context.Update(editALanguage);
                _context.SaveChanges();
                //Display updated view
                return RedirectToAction("Index");
            }            

            return View(myLanguageViewModel);
        }


        public IActionResult RetrieveLanguageDB() 
        {
            //Create List from DB
            var listOfLanguagesFromDB = _context.Languages.ToList();

            return View(listOfLanguagesFromDB);
        }


        public IActionResult AddLanguage()
        {            

            return View();
        }

        [HttpPost]
        public IActionResult AddLanguage(Language language)
        {
            if (ModelState.IsValid) 
            {
                //Make a check that a language doesn't already exist(Languages objects)
                var listOfLanguagesFromDB = _context.Languages.ToList();
                List<string> allPresentLanguagesNames = new List<string>();
                
                //Populate all languages
                foreach (var aLanguage in listOfLanguagesFromDB) 
                {
                    allPresentLanguagesNames.Add(aLanguage.Name);
                }            
                
                //Doesnt contain the language
                if (! allPresentLanguagesNames.Contains(language.Name))
                {
                    _context.Languages.Add(language);
                    _context.SaveChanges();
                    ViewBag.StatusNewLanguage = $"Success - Add Language - The language '{language.Name}' was added";
                }
                else
                {
                    ViewBag.StatusNewLanguage = $"Failure - Add Language - The language '{language.Name}' already exists";
                }
               

                
            }
            else
            {
                ViewBag.StatusNewLanguage = $"Error: Missing/Invalid input in 'language form'";
            }


            return View("RetrieveLanguageDB", _context.Languages.ToList());
        }



    }
}
