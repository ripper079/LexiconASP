using Microsoft.AspNetCore.Mvc;
using AssignmentMVC.Models;

namespace AssignmentMVC.Controllers
{
    public class PersonController : Controller
    {
        public static PeopleViewModel myPeopleView = new PeopleViewModel();
        public static int IDForPeople = 100;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Person()
        {
            return View(myPeopleView);           
        }

        //Adds (if possible) a person
        [HttpPost]
        public IActionResult Person(PeopleViewModel pPeopleViewModel)
        {
            if (ModelState.IsValid) 
            {
                myPeopleView.addPersonToList(
                    pPeopleViewModel.cpvm.FullName, pPeopleViewModel.cpvm.PhoneNumber, pPeopleViewModel.cpvm.City, IDForPeople++
                );
            }
            
            return View(myPeopleView);
        }
       
        public IActionResult RemovePerson(int id) 
        {
            myPeopleView.removePersonFromList(id);
            return View("Person", myPeopleView);
        }

        [HttpPost]
        public IActionResult Filter(string filtertext) 
        {
            //Restore original viewmodel
            if (string.IsNullOrEmpty(filtertext))
            {
                return View("Person", myPeopleView);
            }

            //Create a filtered list based original viewmodel
            var filteredPeople = myPeopleView.listOfPersons.Where
                (x => x.FullName == filtertext || x.City == filtertext).ToList();

            //Create a new filtered viewmodel
            var filteredViewModel = new PeopleViewModel();
            //Set the filtered view 
            filteredViewModel.listOfPersons = filteredPeople;


            return View("Person", filteredViewModel);
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
