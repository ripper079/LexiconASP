﻿using Microsoft.AspNetCore.Mvc;
using AssignmentMVC.Models;             //For testing

namespace AssignmentMVC.Controllers
{
    public class AjaxController : Controller
    {
        public static PeopleViewModel myPeopleView = new PeopleViewModel();
        public static int IDForPeople = 1000;

        public IActionResult Index()
        {
            Console.WriteLine("Hit on AjaxController on Index()");
            //return NotFound("Custom Simulated 404 Not Found Page - In [AjaxController] on [Index()] action");     //"Custom page"
            return View();
            //return View(myPeopleView);
            //return PartialView("_ListAllPersons", myPeopleView);

        }

        [HttpGet]
        public IActionResult Get() 
        {
            Console.WriteLine("Hit on AjaxController on Get()");
            //return NotFound("Custom Simulated 404 Not Found Page - In [AjaxController] on [Get()] action");
            return View("Index");
            //return Json(
            //    new
            //    {
            //        data = "blah blah blah",
            //        date = DateTime.Now
            //    }
            //    );
        }

        //[HttpGet("{id}")]
        //public IActionResult Get(int id)
        //{
        //    Console.WriteLine("Hit on AjaxController on Get(int id)");
        //    return NotFound("Custom Simulated 404 Not Found Page - In [AjaxController] on [Get(int id)] action");
        //}

        //For testing
        public JsonResult MyJson() 
        {
            Person myPerson = new Person() 
            {
                FullName = "Jörgen Jönsson",
                City = "Stockholm",
                PhoneNumber = "031-330330",
                IdPerson = 999
            };

            return Json(myPerson);
        }

        //Testing!!!
        public String MyNameIsCool() 
        {
            return "Daniel Cool Oikarainen";
        }

        public IActionResult GetAllPersons()
        {
            return PartialView("_ListPersonsWithoutId", myPeopleView);
        }

        public IActionResult GetOnePersons(int id)
        {
            //Create a filtered list of ONE person based original viewmodel
            var filteredPeople = myPeopleView.listOfPersons.Where
                (aPeople => aPeople.IdPerson == id).ToList();

            //Create a new filtered viewmodel
            var filteredViewModel = new PeopleViewModel();
            //Set the filtered view 
            filteredViewModel.listOfPersons = filteredPeople;


            return PartialView("_ListPersonsWithoutId", filteredViewModel);
        }

        [HttpPost]
        public string RemovePerson(int id)
        {
            if (IsPersonValid(id)) {
                myPeopleView.removePersonFromList(id);
                return "The person with id" + id + " was deleted";
            }

            return "No person removed. Id was incorrect";
        }

        private bool IsPersonValid(int prospectId) 
        {
            foreach(var item in myPeopleView.listOfPersons)
            {
                if (item.IdPerson == prospectId) 
                {
                    return true;
                }
            }
            return false;
            
        }
    }
}