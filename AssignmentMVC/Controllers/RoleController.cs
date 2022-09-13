using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AssignmentMVC.Models;

namespace AssignmentMVC.Controllers
{
    public class RoleController : Controller
    {
        readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        //Prints a list of all roles
        public IActionResult Index()
        {
            var allRoles = _roleManager.Roles;

            return View(allRoles);
        }

        //This will display the form create new Role
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleViewModel myCreateRoleViewModel) 
        {

            if (ModelState.IsValid) 
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(myCreateRoleViewModel.RoleName));

                //Check if entry is made to the database
                if (result.Succeeded) 
                {
                    return RedirectToAction("index");
                }

                //Add the error to the ModelState - key is empty
                foreach (IdentityError anError in result.Errors) 
                {
                    ModelState.AddModelError("", anError.Description);
                }
            }


            //Try again create a new role
            return View(myCreateRoleViewModel);
        }

        
    }
}
