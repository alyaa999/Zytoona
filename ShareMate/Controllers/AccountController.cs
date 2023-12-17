
using System;
using System.Data.Common;
using System.Security.Claims;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

using Microsoft.IdentityModel.Tokens;
using ShareMate.Models;
using ShareMate.DataTransferObject;
using ShareMate.DbContext;

namespace Tutorials.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]


    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly DbContextApplication dbContextApplication;

        public AccountController(UserManager<User> userManager, IConfiguration configuration , DbContextApplication dbContextApplication)
        {
            _userManager = userManager;
            _configuration = configuration;
            this.dbContextApplication = dbContextApplication;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto userDto)
        {
            try
            {
                if (ModelState.IsValid)
                {



                    var user = new User();
                    user.Email = userDto.email;
                    user.Bio = userDto.bio;
                    user.UserName = userDto.username;
                    user.Level = userDto.level;
                    user.Department = userDto.department;
                    IdentityResult result = await _userManager.CreateAsync(user, userDto.password);
                    dbContextApplication.SaveChanges();

                    User NewUser = dbContextApplication.Users.Where(i => i.UserName == userDto.username).FirstOrDefault();
                    Student student = new Student();
                    student.UserId = NewUser.Id;
                    dbContextApplication.Students.Add(student);
                    dbContextApplication.SaveChanges();
                    if (result.Succeeded)
                    {
                        return Ok("Account Add Success");
                    }
                    else return BadRequest(result);

                }
                else return BadRequest(ModelState);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto userDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(userDto.Username);
                if (user != null)
                {
                    bool found = await _userManager.CheckPasswordAsync(user, userDto.Password);
                    if (found)
                    {
                        var claims = new[] {
                        new Claim(ClaimTypes.Name, userDto.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) ,
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };

                        var roles = await _userManager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            Claim claim = new Claim(JwtRegisteredClaimNames.Name, userDto.Username);
                            claims.Append(claim);
                        }
                        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));


                        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken Token = new JwtSecurityToken(
                            issuer: _configuration["Jwt:Issuer"],
                            audience: _configuration["Jwt:Audience"],
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(120),
                            signingCredentials: signingCredentials
                        );
                        User curUser = dbContextApplication.Users.Where(i => i.UserName == userDto.Username).FirstOrDefault();   

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(Token),
                            expiration = Token.ValidTo,
                            username = userDto.Username

                        }); ;


                    }
                }
                return Unauthorized();

            }
            return Unauthorized();

        }

    }
}
