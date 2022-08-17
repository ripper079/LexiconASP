using Microsoft.AspNetCore.Mvc;

namespace AssignmentMVC.Controllers
{
    public class DoctorController : Controller
    {

        //GET by default
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FeverCheck()
        {
            return View();
        }        

        [HttpPost]
        public IActionResult FeverCheck(int? humantemperature, string temperatureunit)
        {

            //Get temp in Celcius
            if (humantemperature != null) 
            {
                ViewBag.patient = AssignmentMVC.Models.Utilities.GetTemperatureStatusInCelsius(humantemperature);
            }
            

            return View();

        }

    }
}
