using Management.Data;
using Management.Models;
using Management.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net.Mail;

namespace Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]

    public class ManageUser : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ManageUser(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Route("/[controller]/view")]
        [HttpGet]
        public async Task<IActionResult> view(string userid)
        {
            var books = await _context.Users.FromSqlInterpolated($"SELECT * FROM [security].Books ").ToListAsync();
            return Ok(books);
        }


        [Route("/[controller]/deletebyid/{userid}")]
        [HttpDelete]
        public async Task<IActionResult> delete(string userid)
        {

            var user = await _userManager.FindByIdAsync(userid);
            if (user == null) { return NotFound(); }

            var notadmin = await _context.UserRoles.FromSqlInterpolated($"SELECT * FROM [security].UserRoles WHERE UserId={userid}And RoleId='fa6ae62d-7a52-4a6e-8e13-97086c081c35'").ToListAsync();
            if(notadmin.Count != 0) { return BadRequest("you can't delete admin :)"); }


            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception();
            }
            _context.SaveChanges();
            return Ok(user);
        }

        [Route("/[controller]/add")]
        [HttpPost]

        public async Task<IActionResult> Add([FromForm] AddUserViewModel model)
        {

            if (!ModelState.IsValid)
            {
                var response = new
                {
                    Message = "somethisg wrong",
                    newuser = model

                };
                return BadRequest();
            }

            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                var response = new
                {
                    Message = "Email is alredy exists",
                    newuser = model.Email

                }; return BadRequest("Email is alredy exists");
            }
            var user = new AppUser
            {
                UserName = new MailAddress(model.Email).User,
                Email = model.Email,
                Fname = model.Fname,
                Lname = model.Lname

            };
            var result = await _userManager.CreateAsync(user, model.Password);

           

            await _userManager.AddToRoleAsync(user, "User");
            _context.SaveChanges();


            return Ok(user);
        }

        [Route("/[controller]/update")]
        [HttpPost]
        public async Task<IActionResult> Modified([FromForm] ModifiedUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) { return NotFound(); }

            var usermail = await _userManager.FindByEmailAsync(model.Email);
            if (usermail != null && usermail.Id != model.Id)
            {
                ModelState.AddModelError("Email", "Email is alredy exists");
                return BadRequest(model);
            }
            if(model.Password!=model.ConfirmPassword)
                return BadRequest("The password and confirmation password do not match.");

            user.Fname = model.Fname;
            user.Email = model.Email;
            user.Lname = model.Lname;
            var result = await _userManager.CreateAsync(user, model.Password);

            await _userManager.UpdateAsync(user);
            _context.SaveChanges();
            return Ok(user);
        }

    }
}

