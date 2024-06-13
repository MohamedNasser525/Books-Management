
using Management.Models;
using Management.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Runtime.InteropServices;

namespace Management.Controllers
{

    [Authorize(Roles = "Admin")]

    public class User : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        //        private readonly RoleManager<IdentityUser> _roleManager;

        public User(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            //_roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Select(use => new UserViewModel
            {
                Id = use.Id,
                Name = use.UserName,
                FName = use.Fname,
                LName = use.Lname,
                Email = use.Email,

            }).ToListAsync();

            return View(users);
        }

        public async Task<IActionResult> Add()
        {



            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError("Email", "Email is alredy exists");
                return View(model);
            }
            var user = new AppUser
            {
                UserName = new MailAddress(model.Email).User,
                Email = model.Email,
                Fname = model.Fname,
                Lname = model.Lname

            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("ConfirmPassword", error.Description);
                }
                return View(model);

            }

            await _userManager.AddToRoleAsync(user, "User");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Modified(string UserID)
        {
            var user = await _userManager.FindByIdAsync(UserID);
            if (user == null)
            {
                return NotFound("Wrong User Id");
            }

            var newuser = new ModifiedUserViewModel
            {
                Id = user.Id,
                Fname = user.Fname,
                Email = user.Email,
                Lname = user.Lname,
                //  Password = user.PasswordHash,
               // Phone = user.PhoneNumber
            };

            return View(newuser);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modified(ModifiedUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) { return NotFound(); }

            var usermail=await _userManager.FindByEmailAsync(model.Email);
            if (usermail!=null&& usermail.Id!=model.Id)
            {
                ModelState.AddModelError("Email", "Email is alredy exists");
                return View(model);
            }
            user.Fname = model.Fname;
            user.Email = model.Email;
            user.Lname = model.Lname;
           // user.PhoneNumber = model.Phone;
           
            await _userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }
/*
        [HttpDelete]
        public async Task<IActionResult> Delete(string userid)
        {

            var user=await _userManager.FindByIdAsync(userid);
            if (user==null) { return NotFound(); }
            var result=await _userManager.DeleteAsync(user);
            if(!result.Succeeded) {
                throw new Exception();
            }
            return Ok();
        }
*/
    }
}
