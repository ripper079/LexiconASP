using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AssignmentMVC.Models;
using AssignmentMVC.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace AssignmentMVC.Controllers
{
    [Authorize(Roles = "Admin, Moderator")]
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
                
                //return View("Index");
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

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            //The id will always be valid and role to
            var roleToDelete = await _roleManager.FindByIdAsync(id);
            
            var myDeleteRoleViewModel = new DeleteRoleViewModel
            {
                Id = roleToDelete.Id,
                RoleName = roleToDelete.Name
            };

            return View(myDeleteRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteRoleViewModel myDeleteRoleViewModel) 
        {

            var roleToDelete = await _roleManager.FindByIdAsync(myDeleteRoleViewModel.Id);

            roleToDelete.Name = myDeleteRoleViewModel.RoleName;

            IdentityResult result = await _roleManager.DeleteAsync(roleToDelete);


            if (result.Succeeded)
            {
                return RedirectToAction("index");                

            }

            //Add the error to the ModelState - key is empty
            foreach (IdentityError anError in result.Errors)
            {
                ModelState.AddModelError("", anError.Description);
            }

            return View(myDeleteRoleViewModel);
        }


        public IActionResult ListUsers()
        {
            var allRegistratedUsers = _userManager.Users;


            return View(allRegistratedUsers);
        }

        [HttpGet]
        public async Task<IActionResult> AddRoleToUser(string id) 
        {
            //The user
            var theUserToAddARole = await _userManager.FindByIdAsync(id);
            //Get all roles
            var allAvailibleRoles = _roleManager.Roles;

            List<string> allStringRoles = new List<string>();
            StringBuilder userCurrentRoles = new StringBuilder();

            //Create string for displaying currentRoles
            // P.S There is already an open DataReader associated with this Connection which must be closed first error occurs if try 
            //if (await _userManager.IsInRoleAsync(theUserToAddARole, aStringRole)) in this below foreach...
            foreach (var role in allAvailibleRoles)
            {
                allStringRoles.Add(role.Name); 
            }

            //Workaround
            foreach (var aStringRole in allStringRoles)
            {
                if (await _userManager.IsInRoleAsync(theUserToAddARole, aStringRole))
                {
                    userCurrentRoles.Append(aStringRole);
                    userCurrentRoles.Append(" ");
                }
            }

            UserRoleViewModel myUserRoleViewModel = new UserRoleViewModel();

            myUserRoleViewModel.UserId = theUserToAddARole.Id;
            myUserRoleViewModel.UserName = theUserToAddARole.UserName;
            myUserRoleViewModel.FirstName = theUserToAddARole.FirstName;
            myUserRoleViewModel.LastName = theUserToAddARole.LastName;
            myUserRoleViewModel.Email = theUserToAddARole.Email;


            ViewBag.ListOfRoles = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
            ViewBag.CurrentRoles = userCurrentRoles.ToString();

            return View(myUserRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleToUser(UserRoleViewModel myUserRoleViewModel) 
        {            
            var user = await _userManager.FindByIdAsync(myUserRoleViewModel.UserId);
            string roleNameToAdd = myUserRoleViewModel.RoleName;

            if (ModelState.IsValid) 
            {
                if (string.IsNullOrEmpty(roleNameToAdd))
                {
                    RedirectToAction("index", "role");
                }
                else
                {
                    //Check that user doesnt already have this role
                    if (! (await _userManager.IsInRoleAsync(user, roleNameToAdd)))
                    {
                        await _userManager.AddToRoleAsync(user, myUserRoleViewModel.RoleName);
                        ViewBag.RoleAddedForUser = $"Success: The user {user.FirstName} {user.LastName} was added to role '{roleNameToAdd}'";
                    }
                    else
                    {
                        ViewBag.RoleAddedForUser = $"Failure: The user {user.FirstName} {user.LastName} has already the role '{roleNameToAdd}'";
                    }

                    return View("ListUsers", _userManager.Users);

                }

            }
            
            //Enter Valid data again
            ViewBag.ListOfRoles = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
            return View(myUserRoleViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRoleFromUser(string id) 
        {

            //The user
            var theUserToRemoveARole = await _userManager.FindByIdAsync(id);
            //Get all roles
            var allAvailibleRoles = _roleManager.Roles;

            //If no roles then redirect 
            if (allAvailibleRoles.Count() == 0) 
            {
                return View("ListUsers", _userManager.Users);
            }
            
            List<string> allStringRoles = new List<string>();
            StringBuilder userCurrentRoles = new StringBuilder();

            //Create string for displaying currentRoles
            // P.S There is already an open DataReader associated with this Connection which must be closed first error occurs if try 
            //if (await _userManager.IsInRoleAsync(theUserToAddARole, aStringRole)) in this below foreach...
            foreach (var role in allAvailibleRoles)
            {
                allStringRoles.Add(role.Name);
            }

            //Workaround
            foreach (var aStringRole in allStringRoles)
            {
                if (await _userManager.IsInRoleAsync(theUserToRemoveARole, aStringRole))
                {
                    userCurrentRoles.Append(aStringRole);
                    userCurrentRoles.Append(" ");
                }
            }

            UserRoleViewModel myUserRoleViewModel = new UserRoleViewModel();

            myUserRoleViewModel.UserId = theUserToRemoveARole.Id;
            myUserRoleViewModel.UserName = theUserToRemoveARole.UserName;
            myUserRoleViewModel.FirstName = theUserToRemoveARole.FirstName;
            myUserRoleViewModel.LastName = theUserToRemoveARole.LastName;
            myUserRoleViewModel.Email = theUserToRemoveARole.Email;


            ViewBag.ListOfRoles = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
            ViewBag.CurrentRoles = userCurrentRoles.ToString();

            return View(myUserRoleViewModel);


        }

        [HttpPost]
        public async Task<IActionResult> DeleteRoleFromUser(UserRoleViewModel myUserRoleViewModel) 
        {
            var user = await _userManager.FindByIdAsync(myUserRoleViewModel.UserId);
            string roleNameToDelete = myUserRoleViewModel.RoleName;

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(roleNameToDelete))
                {
                    RedirectToAction("index", "role");
                }
                else
                {
                    //Check that user doesnt already have this role
                    if (await _userManager.IsInRoleAsync(user, roleNameToDelete))
                    {
                        await _userManager.RemoveFromRoleAsync(user, myUserRoleViewModel.RoleName);
                        ViewBag.RoleDeletedForUser = $"Success: The role '{roleNameToDelete}' was removed from {user.FirstName} {user.LastName}";
                    }
                    else
                    {
                        ViewBag.RoleDeletedForUser = $"Failure: The role '{roleNameToDelete}' doesny exist for {user.FirstName} {user.LastName}";
                    }

                    return View("ListUsers", _userManager.Users);

                }

            }

            //Enter Valid data again
            ViewBag.ListOfRoles = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
            return View(myUserRoleViewModel);

        }

    }
}
