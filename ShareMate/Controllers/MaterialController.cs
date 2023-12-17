using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShareMate.DataTransferObject;
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
        public async Task<IActionResult> Create(materialD MaterialDto)
        {

            try
            {
                int  courseId = dbContext.Courses.Where(i => i.Title ==MaterialDto.course).Select(i=>i.Id).FirstOrDefault();
                User user = dbContext.Users.Where(i => i.UserName == MaterialDto.username).FirstOrDefault();
                Student CurrStudent = dbContext.Students.FirstOrDefault(i => i.UserId ==user.Id );
                Material material = new Material();
                material.Description = MaterialDto.title;
                material.Path = MaterialDto.url;
                material.CourseId = courseId;
                material.StudentId = CurrStudent.Id;
                material.Type = MaterialDto.type;

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
        public async Task<IActionResult> ViewSpecificMaterialsType(int courseId, int type)
        {
            try
            {
                List<Material> materials = dbContext.Materials
                    .Where(i => i.CourseId == courseId && i.Type == type)
                    .ToList();

                List<MaterialtypeDto> materialtypeDtos = new List<MaterialtypeDto>();

                foreach (Material material in materials)
                {
                    MaterialtypeDto materialtypeDto = new MaterialtypeDto
                    {
                        title = material.Description,
                        url = material.Path
                    };

                    // Find the associated student and user
                    Student student = dbContext.Students.FirstOrDefault(s => s.Id == material.StudentId);
                    if (student != null)
                    {
                        User user = dbContext.Users.FirstOrDefault(u => u.Id == student.UserId);
                        if (user != null)
                        {
                            materialtypeDto.username = user.UserName;
                        }
                    }

                    materialtypeDtos.Add(materialtypeDto);
                }

                return Ok(JsonConvert.SerializeObject(materialtypeDtos));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
