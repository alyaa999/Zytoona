using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShareMate.IdentityRepo;
using ShareMate.Models;
using System.Reflection;
using System;
using ShareMate.DataTransferObject;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ShareMate.Controllers
{

    [Route("api/[controller]")]
        [ApiController]
        public class AccountController : ControllerBase
        {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            // Validation logic for the LoginDto
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponseDto { Success = false, Message = "Invalid login request" });
            }

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // Check if the user has an existing cookie or if the cookie has expired
                var existingCookie = HttpContext.Request.Cookies[CookieAuthenticationDefaults.AuthenticationScheme];

                if (string.IsNullOrEmpty(existingCookie))
                {
                    // User doesn't have a cookie or the cookie has expired, create a new one
                    var user = await _userManager.FindByNameAsync(model.Username);


                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                // Add more claims if needed
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true, // You can set this based on your requirements
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties
                    );
                }
                

                return Ok(new ApiResponseDto { Success = true, Message = "Login successful" });
            }
            else
            {
                return BadRequest(new ApiResponseDto { Success = false, Message = "Invalid login attempt" });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            // Validation logic for the RegisterDto
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponseDto { Success = false, Message = "Invalid registration request" });
            }

            var user = new User { UserName = model.Username, Email = model.Username , Bio = model.Bio , Department = model.Department , Level = model.Level };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Check if this is the user's first registration
                var isFirstTime = !await _userManager.IsEmailConfirmedAsync(user);

                // If it's the first time, set a cookie
                if (isFirstTime)
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                // Add more claims if needed
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true, // You can set this based on your requirements
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties
                    );
                }

                // You may choose to sign in the user after registration
                // await _signInManager.SignInAsync(user, isPersistent: false);

                return Ok(new ApiResponseDto { Success = true, Message = "Registration successful" });
            }
            else
            {
                return BadRequest(new ApiResponseDto { Success = false, Message = "Error during registration" });
            }
        }



    }
}
