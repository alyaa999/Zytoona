using Microsoft.AspNetCore.Mvc;
using ShareMate.DbContext;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShareMate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController
    {
        private readonly DbContextApplication dbContextApplication;

        public StudentController( DbContextApplication dbContextApplication ) 
        {
            this.dbContextApplication = dbContextApplication;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudent()
        {
            // Access the DefaultUserId property
            var userId = DefaultUserId;

            // Your action logic here
            return Ok();
        }
    }
}
