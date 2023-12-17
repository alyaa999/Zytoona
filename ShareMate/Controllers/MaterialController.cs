using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShareMate.DbContext;
using ShareMate.Models;

namespace ShareMate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : BaseController
    {
        private readonly DbContextApplication dbContext;

        public MaterialController(DbContextApplication dbContextApplication)
        {
            this.dbContext = dbContextApplication;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(string userId , string  course , int type, string title , string url )
        {

            try
            {
                userId = DefaultUserId;
                int  courseId = dbContext.Courses.Where(i => i.Title ==course).Select(i=>i.Id).FirstOrDefault();  
                Student CurrStudent = dbContext.Students.FirstOrDefault(i => i.UserId == userId);
                Material material = new Material();
                material.Description = title;
                material.Path = url;
                material.Date = DateTime.Now;
                material.CourseId = courseId;
                material.StudentId = CurrStudent.Id;
                material.Type = type;

                var response  = dbContext.Materials.Add(material);
                dbContext.SaveChanges();
                return Ok("done create ");

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);  

            }


        }

        [HttpGet("ViewSpecificMaterialsType")]
        public async Task<IActionResult> ViewSpecificMaterialsType (int courseId , int type)
        {
            try
            {

                List<Material> materials = dbContext.Materials.Where(i => i.CourseId == courseId && i.Type == type).ToList();
                return Ok(JsonConvert.SerializeObject(materials));
            }
            catch (Exception ex) { 
             return BadRequest(ex.Message); 
            }

        }
        
    }
}
