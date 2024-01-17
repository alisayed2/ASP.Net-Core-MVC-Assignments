using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels;

namespace MVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {


        /* 
Steps : 
1- Create Role
2- Assign Role To User 
3- Check Action Role 
*/
        private readonly Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager;
        public RolesController(RoleManager<IdentityRole> roleManager) 
        {
            this.roleManager = roleManager;
        }

        // Create Role
        // Link
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        // Submit
        [HttpPost]
        public async Task<IActionResult> New(RoleViewModel newRoleVM)
        {
            if (!ModelState.IsValid)
            {
                return View(newRoleVM);
            }
            else
            {
                IdentityRole role = new IdentityRole();
                role.Name = newRoleVM.Name;

                IdentityResult result = await roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    foreach(var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                    return View(newRoleVM);
                }

                    return View(new RoleViewModel());

            }
            
        }


    }
}
