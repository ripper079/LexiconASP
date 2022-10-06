using AssignmentMVC.Data;
using AssignmentMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

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

        
        [Route("addapesontodb")]
        [ProducesResponseType(200)]
        [HttpPost]
        //public string CreateAPerson(CreatePersonFrontEnd myCreateFE)
        public ActionResult<Person> CreateAPerson(CreatePersonFrontEnd myCreateFE)
        {
            Console.WriteLine("Hit on CreateAPerson...");
            var newPerson = new Person()
            {
                FullName = myCreateFE.FullName,
                PhoneNumber = myCreateFE.PhoneNumber,
                City_Id = myCreateFE.CityId

            };

            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.WriteLine($"Person created...FullName={newPerson.FullName} PhoneNumber={newPerson.PhoneNumber} CityId={newPerson.City_Id} ");

            Response.StatusCode = 200;

            return newPerson;


        }

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

        [HttpGet("saymyname")]
        [ProducesResponseType(200)]
        public ActionResult<string> GetMyCoolName(int id)
        {
            return "Dan the cool man";
        }



        [HttpGet("persondetails/{id}")]        
        [ProducesResponseType(200)]
        public ActionResult<Person> Get(int id)
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

           Person myPerson = _context.People.FirstOrDefault(aPerson => aPerson.IdPerson == id);

            return myPerson;
        }                


        [HttpGet("getallpeople")]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<Person>> GetAllPeople(int id)
        {

            //List<Person> people = _context.People
            //    .Include(x => x.Languages)
            //    .Include(x => x.CityOfPerson)                
            //    .ToList();




            //var query =
            //(
            //        from aRowPerson in _context.People
            //        join aRowCity in _context.Cities
            //            on aRowPerson.City_Id equals aRowCity.Id
            //        join aRowCountry in _context.Contries
            //            on aRowCity.Country_Id equals aRowCountry.Id

            //        //Create new 'record'
            //        select new
            //        {
            //            IdPerson = aRowPerson.IdPerson,
            //            FullName = aRowPerson.FullName,
            //            PhoneNumber = aRowPerson.PhoneNumber,
            //            CityName = aRowCity.CityName,
            //            CountryName = aRowCountry.CountryName
            //        }

            //);

            //var queryList = query.ToList();

            //foreach (var item in queryList)
            //{
            //    Console.WriteLine($"Id person= {item.IdPerson} FullName= {item.FullName} PhoneNumber= {item.PhoneNumber} CityName= {item.CityName} CountryName= {item.CountryName}");
            //}


            return _context.People.ToList();
        }


        [HttpGet("getallcountries")]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<Country>> GetAllCountries() 
        {
            return _context.Contries.ToList();
        }

        [HttpGet("abcgetallcountries")]
        [ProducesResponseType(200)]
        public async Task <IActionResult> ABCGetAllCountries()
        {
            var testallcountries = await _context.Contries.ToListAsync();
            return Ok(testallcountries);
        }

        [HttpGet("getallcities")]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<City>> GetAllCities()
        {
            return _context.Cities.ToList();
        }


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

    }
}
