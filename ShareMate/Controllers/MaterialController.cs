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
        public async Task<IActionResult> Create(string userId , int courseId , int Type, string description , string path )
        {

            try
            {
                userId = DefaultUserId;
                Student CurrStudent = dbContext.Students.FirstOrDefault(i => i.UserId == userId);
                Material material = new Material();
                material.Description = description;
                material.Path = path;
                material.Date = DateTime.Now;
                material.CourseId = courseId;
                material.StudentId = CurrStudent.Id;
                material.Type = Type;

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
