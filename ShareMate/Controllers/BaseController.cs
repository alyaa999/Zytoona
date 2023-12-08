using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShareMate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected string DefaultUserId { get; }

        public BaseController()
        {
            this.DefaultUserId = "bf092286-cee1-467c-aaf2-9cbe55235dd3";
        }
    }
}
