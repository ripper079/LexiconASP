using Microsoft.AspNetCore.Mvc;
using AssignmentMVC.Models;             //For testing

namespace AssignmentMVC.Controllers
{
    public class AjaxController : Controller
    {
        public IActionResult Index()
        {
            Console.WriteLine("Hit on AjaxController on Index()");
            //return NotFound("Custom Simulated 404 Not Found Page - In [AjaxController] on [Index()] action");     //"Custom page"
            return View();

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
                FullName = "Görgen Jönsson",
                City = "Stockholm",
                PhoneNumber = "031-330330",
                IdPerson = 999
            };

            return Json(myPerson);
        }

        public String MyNameIsCool() 
        {
            return "Daniel";
        }
    }
}
