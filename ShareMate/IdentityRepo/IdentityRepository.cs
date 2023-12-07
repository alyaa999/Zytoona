using Microsoft.AspNetCore.Identity;
using ShareMate.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using static ShareMate.IdentityRepo.IdentityRepository;

using ShareMate.DbContext;

namespace ShareMate.IdentityRepo
{
    public class IdentityRepository : IIdentityInterface 
    {


       
            private readonly DbContextApplication _context;

            private readonly IHttpContextAccessor contextAccessor;

            public UserManager<User> UserManager { get; }

            public IdentityRepository(  DbContextApplication context, UserManager<User> _userManager, IHttpContextAccessor contextAccessor)
            {
                _context = context;
                UserManager = _userManager;
                this.contextAccessor = contextAccessor;
            }

            public string GetUserID()
            {
                return contextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            public string GetUserName()
            {
                return contextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.Name);

            }
            public User GetUser()
            {
                User user = UserManager.FindByIdAsync(GetUserID()).Result;
                return user;

            }


        }
    }


