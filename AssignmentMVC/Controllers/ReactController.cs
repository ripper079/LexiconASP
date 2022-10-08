using AssignmentMVC.Data;
using AssignmentMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

//abc indicates that it is a async variant - sorry for bad naming coonvention

namespace AssignmentMVC.Controllers
{
    //localhost:xxxx/api/React/api-endpoint-name
    [Route("[controller]")]
    [ApiController]
    public class ReactController : ControllerBase
    {
        readonly ApplicationDbContext _context; //skapar en readonly av DbContext

        public ReactController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        //Testing mocking units
        [Route("mockaviewmodel")]
        //[ProducesResponseType(200)]
        public string FakePersonInJson()
        {
            Person myPerson = new Person()
            {
                FullName = "Jason Killer",
                PhoneNumber = "666-666",
                IdPerson = 999
            };

            //var result = new JsonResult(myPerson);
            var result = JsonConvert.SerializeObject(myPerson);


            return result;
        }

        //Testing testing testing
        [HttpGet("saymyname")]
        [ProducesResponseType(200)]
        public ActionResult<string> GetMyCoolName(int id)
        {
            return "Daniel is the cool man";
        }


        //Get specified detailed data about a person tailored to react app
        [HttpGet("persondetails/{id}")]        
        [ProducesResponseType(200)]
        public ActionResult<DetailPersonDataReact> Get(int id)
        {
            //Console.WriteLine("HHIIITTTTT");
            //Person myPerson = new Person()
            //{
            //    FullName = "Jörgen Jönsson",
            //    PhoneNumber = "031-330330",
            //    IdPerson = id
            //};

            //Response.StatusCode = 200;            
            //var listPeople = _context.People.ToList();

           Person myPerson = _context.People
                                        .Include(x => x.Languages)
                                        .Include( x => x.CityOfPerson)
                                        .FirstOrDefault(aPerson => aPerson.IdPerson == id);

            Console.WriteLine("Id Person=" + myPerson.IdPerson);
            Console.WriteLine("Name Person=" + myPerson.FullName);
            Console.WriteLine("Phonenumber=" + myPerson.PhoneNumber);
            string allLang = "";
            foreach(var aLanguage in myPerson.Languages)
            {
                Console.Write(" " + aLanguage.Name);
                allLang += " " + aLanguage.Name;
            }
            Console.WriteLine("\nCityId of Person=" + myPerson.City_Id);
            Console.WriteLine("CityName= " + myPerson.CityOfPerson.CityName);

            DetailPersonDataReact retPerson = new DetailPersonDataReact()
            {
                IdPerson = myPerson.IdPerson,
                FullName = myPerson.FullName,
                PhoneNumber = myPerson.PhoneNumber,
                Languages = allLang,
                CityId = myPerson.City_Id,
                CityName = myPerson.CityOfPerson.CityName
            };

            return retPerson;
        }

        //Get a list of all languages from table
        [HttpGet("getalllanguages")]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<Language>> GetAllLanguages()
        {
            return _context.Languages.ToList();
        }

        //Get a list of all languages from table - ASYNC variant
        [HttpGet("abcgetalllanguages")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ABCGetAllLanguages()
        {
            var allLanguages = await _context.Languages.ToListAsync();
            return Ok(allLanguages);
        }

        //Get a list of all people from table
        [HttpGet("getallpeople")]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<Person>> GetAllPeople(int id)
        {            
            return _context.People.ToList();
        }

        //Get a list of all people from table - ASYNC variant
        [HttpGet("abcgetallpeople")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ABCGetAllPeople(int id)
        {
            var allPersons = await _context.People.ToListAsync();

            return Ok(allPersons);
        }


        //Get all cities in the city table
        [HttpGet("getallcountries")]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<Country>> GetAllCountries() 
        {
            return _context.Contries.ToList();
        }

        //Get all cities in the city table - ASYNC Variant
        [HttpGet("abcgetallcountries")]
        [ProducesResponseType(200)]
        public async Task <IActionResult> ABCGetAllCountries()
        {
            var testallcountries = await _context.Contries.ToListAsync();
            return Ok(testallcountries);
        }

        //Get all cities in the city table
        [HttpGet("getallcities")]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<City>> GetAllCities()
        {
            return _context.Cities.ToList();
        }

        //Get all cities in the city table - ASYNC variant
        [HttpGet("abcgetallcities")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ABCGetAllCities()
        {
            var allCities = await _context.Cities.ToListAsync();

            return Ok(allCities);
        }


        //Get all cities for the specific country (specify country_id)
        [HttpGet("getcities/{id}")]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<City>> GetCityWithCountyId(int id)
        {
            //Response.StatusCode = 200;            

            //The id is the country id
            //Person myPerson = _context.People.FirstOrDefault(aPerson => aPerson.IdPerson == id);
            var allCityies = _context.Cities.Where(aCity => aCity.Country_Id == id).ToList();

            return allCityies;
        }

        //Get all cities for the specific country (specify country_id) - ASYNC variant
        [HttpGet("abcgetcities/{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> ABCGetCityWithCountyId(int id)
        {
            //Response.StatusCode = 200;            

            //The id is the country id
            //Person myPerson = _context.People.FirstOrDefault(aPerson => aPerson.IdPerson == id);
            var allCities = await _context.Cities.Where(aCity => aCity.Country_Id == id).ToListAsync();

            return Ok(allCities);
        }

        //Adds a person to the DB - ASYNC variant
        [Route("addapesontodb")]
        [ProducesResponseType(200)]
        [HttpPost]
        //public ActionResult<Person> CreateAPerson(CreatePersonFrontEnd myCreateFE)
        public async Task<ActionResult> CreateAPerson(CreatePersonFrontEnd myCreateFE)
        {
            Console.WriteLine("Hit on CreateAPerson...");
            var newPerson = new Person()
            {
                FullName = myCreateFE.FullName,
                PhoneNumber = myCreateFE.PhoneNumber,
                City_Id = myCreateFE.CityId

            };

            //Console.WriteLine("-----------------------------------------------------------------------------");
            //Console.WriteLine($"Person created...FullName={newPerson.FullName} PhoneNumber={newPerson.PhoneNumber} CityId={newPerson.City_Id} ");

            //Response.StatusCode = 200;
            //Save to DB
            //Creates a new person 
            _context.People.Add(newPerson);
            await _context.SaveChangesAsync();



            //This will be valid becuse this is a 'new' user            
            var language = _context.Languages.FirstOrDefault(aLanguage => aLanguage.Id == myCreateFE.LanguageId);
            newPerson.Languages.Add(language);
            //Save to db
            _context.SaveChanges();


            return Ok(newPerson);

        }

        //Deletes/Removes a specific person from the database - ASYNC variant
        [Route("deletepersonfromdb/{id}")]
        [ProducesResponseType(200)]
        [HttpDelete]
        public async Task<ActionResult> DeleteAPerson(int id)
        {
            var thePersonToDelete = _context.People.FirstOrDefault(aPerson => aPerson.IdPerson == id);

            Console.WriteLine("A Person has been deleted from DB");

            _context.People.Remove(thePersonToDelete);
            await _context.SaveChangesAsync();

            return Ok(thePersonToDelete);

        }
    }
}


