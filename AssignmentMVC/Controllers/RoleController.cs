using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentMVC.Controllers
{
    public class RoleController : Controller
    {
        readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        //This will display the form create new Role
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(int thisShouldBeAViewModel) 
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
