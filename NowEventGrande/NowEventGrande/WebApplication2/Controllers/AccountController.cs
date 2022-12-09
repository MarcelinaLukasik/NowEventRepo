using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApplication2.Services.AuthenticationService;

namespace WebApplication2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationService _authenticationService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IAuthenticationService authenticationService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok("here");
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
                    var res = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, true, false);
                    // await _signInManager.SignInAsync(user, false);
                    return Ok(res);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }

            return BadRequest();

        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(User user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, true, false);

                if (result.Succeeded)
                {
                    var token = await _authenticationService.CreateTokenAsync(user);
                    var test = token;

                    return Ok(new { Token = await _authenticationService.CreateTokenAsync(user) });
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


    }
}
