using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NowEvent.Models;
using NowEvent.Models.Constants;
using NowEvent.Services.AuthenticationService;

namespace NowEvent.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserAuthenticationService _userAuthenticationService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            IUserAuthenticationService userAuthenticationService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userAuthenticationService = userAuthenticationService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var res = await _signInManager
                        .PasswordSignInAsync(user.UserName, user.Password, true, false);
                    return Ok(res);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                ModelState.AddModelError(string.Empty, "Invalid Attempt");
            }
            return BadRequest();
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Dictionary<string,string> userData)
        {
            var user = _userManager.FindByNameAsync(userData[Account.UserName]).Result;
            var isUserValid = _signInManager.UserManager
                .CheckPasswordAsync(user, userData[Account.Password]).Result;
            if (isUserValid)
            {
                var result = await _signInManager.PasswordSignInAsync(userData[Account.UserName], 
                    userData[Account.Password],
                    true, false);

                if (result.Succeeded)
                {
                    return Ok(new { Token = await _userAuthenticationService.CreateTokenAsync(user) });
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return BadRequest(user);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logout");
        }

        [HttpGet("GetCurrentUserId")]
        [Authorize]
        public IActionResult GetCurrentUserId()
        {
            if (HttpContext.User.Identity != null)
            {
                string userId = _userAuthenticationService.GetCurrentUserId(HttpContext.User.Identity.Name!);
                return Ok(userId);
            }
            return BadRequest("Invalid user");
        }

    }
}
