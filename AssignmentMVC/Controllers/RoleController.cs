using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AssignmentMVC.Models;
using AssignmentMVC.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AssignmentMVC.Controllers
{
    public class RoleController : Controller
    {
        readonly RoleManager<IdentityRole> _roleManager;
        readonly UserManager<ApplicationUser> _userManager;    

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {            
            _roleManager = roleManager;
            _userManager = userManager;
        }

        //Prints a list of all roles
        public IActionResult Index()
        {
            var allRoles = _roleManager.Roles;

            return View(allRoles);
        }

        

        //This will display the form 'create new Role'
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

        [HttpGet]
        public async Task<IActionResult> Edit(string id) 
        {
            var roleToEdit = await _roleManager.FindByIdAsync(id);

            //No role found
            if (roleToEdit == null)
            {
                RedirectToAction("Index");
            }

            var myEditRoleViewModel = new EditRoleViewModel
            {
                Id = roleToEdit.Id,
                RoleName = roleToEdit.Name
            };

            return View(myEditRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRoleViewModel myEditRoleViewModel)
        {
            var roleToEdit = await _roleManager.FindByIdAsync(myEditRoleViewModel.Id);

            //No role found
            if (roleToEdit == null)
            {
                RedirectToAction("Index");
            }
            else
            {
                roleToEdit.Name = myEditRoleViewModel.RoleName;
                IdentityResult result = await _roleManager.UpdateAsync(roleToEdit);

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

            return View(myEditRoleViewModel);
        }


        public IActionResult ListUsers()
        {
            var allRegistratedUsers = _userManager.Users;
            return View(allRegistratedUsers);
        }

        [HttpGet]
        public async Task<IActionResult> AddRoleToUser(string id) 
        {
            var theUserToAddARole = await _userManager.FindByIdAsync(id);

            UserRoleViewModel myUserRoleViewModel = new UserRoleViewModel();

            myUserRoleViewModel.UserId = theUserToAddARole.Id;
            myUserRoleViewModel.UserName = theUserToAddARole.UserName;
            myUserRoleViewModel.FirstName = theUserToAddARole.FirstName;
            myUserRoleViewModel.LastName = theUserToAddARole.LastName;
            myUserRoleViewModel.Email = theUserToAddARole.Email;


            ViewBag.ListOfRoles = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");

            return View(myUserRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleToUser(UserRoleViewModel myUserRoleViewModel) 
        {
            //The user
            var user = await _userManager.FindByIdAsync(myUserRoleViewModel.UserId);
            
            string roleName = myUserRoleViewModel.RoleName;
            
            //No role exist
            if (string.IsNullOrEmpty(roleName))
            {
                RedirectToAction("index");
            }
            else
            {
                //Only add if user is NOT a member in the role already
                if (! (await _userManager.IsInRoleAsync(user, myUserRoleViewModel.RoleName)))
                {
                    Console.WriteLine($"The user {user.FirstName} {user.LastName} was added to role {roleName}");
                    await _userManager.AddToRoleAsync(user, myUserRoleViewModel.RoleName);
                    
                }
                
            }

            return View("ListUsers", _userManager.Users);
            //return View(myUserRoleViewModel);
        }

    }
}
