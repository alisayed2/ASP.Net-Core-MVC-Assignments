using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MVC.Models;
using MVC.ViewModels;

namespace MVC.Controllers
{
        /*
        * 1- Install package Microsoft.Identity.EntityFramowrkCore
        * 2- Create class ApplicationUser:IDentityUser
        * 3- Edit ITIEnitity :IDenityDbContext
        * 4- Migration
        * 5- AccountControlel
        * 5- 2 actio registration
        * 7- ViewModel RegistrationUser
        */
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        // inject UserManager
        // inject SignInManager
        public AccountController
            (UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ApplicationUserViewModel newUser)

        {
            if (!ModelState.IsValid)
            {
                return View(newUser);
            }

            else
            {
                // Create Account
                ApplicationUser userModel = new ApplicationUser();
                userModel.UserName = newUser.UserName;
                userModel.PasswordHash = newUser.Password;
                userModel.Faculty = newUser.Faculty;
                // UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser> ();
                // make inject Because The Constructor Need Many Parameters
                IdentityResult result = await userManager.CreateAsync(userModel, newUser.Password);
                // IdentityResult check if usermanager assign userModel in database or not
                // second overload To Hash Password in database
                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(newUser);
                }

                // Create Cookie
                //SignInManager<ApplicationUser> Cookie = new SignInManager<ApplicationUser>();
                // make inject 
                // make inject Because The Constructor Need Many Parameters

                await signInManager.SignInAsync(userModel, false);  // SignInAsync ==> create cookie
                                                                     // SignOutAsync ==> delete cookie
                return RedirectToAction("LogIn");
            }
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LogInViewModel userVM)
        {
            if (!ModelState.IsValid)
            {
                return View(userVM);
            }

            else
            {
                // Check if userName Nad Password are Valid
                // 1- check username
                ApplicationUser userModel = await userManager.FindByNameAsync(userVM.UserName);
                // 2- check if userVM has smae password of userModel
                if(userModel == null)
                {
                    ModelState.AddModelError("", "UserName Or Password Invalid");
                    return View(userVM);
                }
                
                else
                {
                    bool found = await userManager.CheckPasswordAsync(userModel, userVM.Password);
                    if (!found)
                    { return View(userVM); }

                    else
                    {
                        await signInManager.SignInAsync(userModel, userVM.RememberMe);
                        return RedirectToAction("Index", "Course");
                    }

                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("LogIn");
        }

        // Add Admin 
        [HttpGet]
        public IActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]      // Must Be Admin 
        public async Task<IActionResult> AddAdmin(ApplicationUserViewModel newUser)
        {
            if (!ModelState.IsValid)
            {
                return View(newUser);
            }

            else
            {
                // Create Account
                ApplicationUser userModel = new ApplicationUser();
                userModel.UserName = newUser.UserName;
                userModel.PasswordHash = newUser.Password;
                userModel.Faculty = newUser.Faculty;
                // UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser> ();
                // make inject Because The Constructor Need Many Parameters
                IdentityResult result = await userManager.CreateAsync(userModel, newUser.Password);
                // IdentityResult check if usermanager assign userModel in database or not
                // second overload To Hash Password in database
                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(newUser);
                }

                // Create Cookie
                //SignInManager<ApplicationUser> Cookie = new SignInManager<ApplicationUser>();
                // make inject 
                // make inject Because The Constructor Need Many Parameters

                await userManager.AddToRoleAsync(userModel,"Admin"); //Assign To Role must check like IdentityResult result = await userManager.CreateAsync(userModel, newUser.Password);
                await signInManager.SignInAsync(userModel, false);  // SignInAsync ==> create cookie
                                                                    // SignOutAsync ==> delete cookie
                return RedirectToAction("LogIn");
            }
        }



        //public async Task<IActionResult> Register(ApplicationUserViewModel newUser)

        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Create Account
        //        ApplicationUser userModel = new ApplicationUser();
        //        userModel.UserName = newUser.UserName;
        //        userModel.PasswordHash = newUser.Password;
        //        userModel.Faculty = newUser.Faculty;
        //        // UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser> ();
        //        // make inject Because The Constructor Need Many Parameters
        //        IdentityResult result = await userManager.CreateAsync(userModel, newUser.Password);//To Hash Password
        //        if (result.Succeeded)
        //        {
        //            // Create Cookie
        //            //SignInManager<ApplicationUser> Cookie = new SignInManager<ApplicationUser>();
        //            // make inject 
        //            // make inject Because The Constructor Need Many Parameters

        //            await signInManager.SignInAsync(userModel, false);  // SignInAsync ==> create cookie
        //                                                                // SignOutAsync ==> delete cookie
        //            return RedirectToAction("Index", "Course");
        //        }
        //        else
        //        {
        //            foreach (var err in result.Errors)
        //            {
        //                ModelState.AddModelError("", err.Description);

        //            }

        //        }
        //    }

        //    return View(newUser);

        //}
    }
}
