using AssignmentMVC.Data;
using AssignmentMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentMVC.Controllers
{
    //localhost:xxxx/api/React/api-endpoint-name
    [Route("api/[controller]")]
    [ApiController]
    public class ReactController : ControllerBase
    {
        readonly ApplicationDbContext _context; //skapar en readonly av DbContext

        public ReactController(ApplicationDbContext context)
        {
            _context = context;
        }


        //public JsonResult FakePersonInJson()
        //{
        //    Person myPerson = new Person()
        //    {
        //        FullName = "Jörgen Jönsson",
        //        PhoneNumber = "031-330330",
        //        IdPerson = 999
        //    };

        //    return Json(myPerson);
        //}

        [HttpGet("people/{id}")]        
        [ProducesResponseType(200)]
        public ActionResult<Person> Get(int id)
        {
            Console.WriteLine("HHIIITTTTT");
            Person myPerson = new Person()
            {
                FullName = "Jörgen Jönsson",
                PhoneNumber = "031-330330",
                IdPerson = id
            };

            Response.StatusCode = 200;

            return myPerson;
        }

    }
}
