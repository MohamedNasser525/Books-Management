using Management.Areas.Identity.Pages.Account;
using Management.Data;
using Management.Models;
using Management.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Management.Controllers
{
    [ApiController]
    [Route("User")]
    public class ForUser : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private new List<string> _allowExtentions = new List<string> { ".jpg", ".png" };
        private long _allowSize = 1048576 *3; // 3m
        public ForUser(ILogger<LoginModel> logger, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _logger = logger;

        }

        [Route("/login")]
        [HttpGet]
       public async Task<IActionResult> login([FromForm] loginbody lobody)
        {
            var username = new EmailAddressAttribute().IsValid(lobody.Email) ? new MailAddress(lobody.Email).User : lobody.Email;
            //remove ^

            if (username==null)
                return BadRequest("email or pass Invalid !!");
            var result = await _signInManager.PasswordSignInAsync(username, lobody.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");

                var user  = await _userManager.FindByEmailAsync(lobody.Email);
                var response = new
                {
                    Message = "you pass",
                   
                    DeletedBook = user,
                    //^
                    Status = "all ok"
                   // Status = True
                };
                return Ok(response);
            }
            return BadRequest("email or pass Invalid !!");

            /*
              var response = new
                {
                    Message = "email or pass Invalid",
                    Status = false
            "
                };
             
             */

        }


        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> register([FromForm]  AddUserViewModel model)
        {

            if (!ModelState.IsValid)
            {
                var response = new
                {
                    Message = "somethisg wrong",
                    newuser = model

                };
                return BadRequest(response);
            }

            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                var response = new
                {
                    Message = "Email is alredy exists",
                    newuser = model.Email

                }; return BadRequest(response);
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

        [HttpPut]
        [Route("/profilepicture/{id}")]
        public async Task<IActionResult> profilepicture(string id,[FromForm] picture bot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("somethig be wrong");
            }
            if (bot.poster == null)
                return BadRequest("poster is required !!");

            if (!_allowExtentions.Contains(Path.GetExtension(bot.poster.FileName).ToLower()))
                return BadRequest("only allow Extentions be .jpg or .png !!");

            if (bot.poster.Length > _allowSize)
                return BadRequest("Max size of poster 3m !!");

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return BadRequest($"id not correct");
           

            using var datastream = new MemoryStream();
            await bot.poster.CopyToAsync(datastream);
            user.Profileimg = datastream.ToArray();

            await _userManager.UpdateAsync(user);
            _context.SaveChanges();
            return Ok(user);

        }

        [Route("/update")]
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
            if (model.Password != model.ConfirmPassword)
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
