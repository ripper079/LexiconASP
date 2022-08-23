using Microsoft.AspNetCore.Mvc;
using AssignmentMVC.Models;

namespace AssignmentMVC.Controllers
{
    public class PersonController : Controller
    {
        public static PeopleViewModel myPeopleView = new PeopleViewModel();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Person()
        {

            return View(myPeopleView);           
        }

        [HttpPost]
        public IActionResult Person(string fullname, string phonenumber, string city)
        {
            myPeopleView.addPersonToList(fullname, phonenumber, city );
            return View(myPeopleView);
        }
       
        public IActionResult RemovePerson(int id) 
        {
            myPeopleView.removePersonFromList(id);
            return View("Person", myPeopleView);
        }

        [HttpPost]
        public IActionResult Filter(string filterbyfullname, string filterbyphonenumber, string filterbycity) 
        {
            if (ModelState.IsValid) 
            {

            }

            myPeopleView.filterList(filterbyfullname, filterbyphonenumber, filterbycity);
            return View("Person", myPeopleView);
        }

        [HttpGet]
        public IActionResult AddPerson()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddPerson(Person Create)
        {
            Console.WriteLine(Create);
            return View();
        }
    }
}
