using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShareMate.DataTransferObject;
using ShareMate.DbContext;
using ShareMate.Models;

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

        [HttpGet("ViewProfile")]
        public async Task<IActionResult> ViewProfile(string userId)  // view profile details + enroll courses  + upload materials + counts 
        {
            try
            {
                userId = DefaultUserId; // will remove later .. 
                User UserDetails = dbContextApplication.Users.Where(i => i.Id == userId).FirstOrDefault();
                Student CurStudent = dbContextApplication.Students.Where(i => i.UserId == userId).FirstOrDefault();
                List<Enroll> favouraites = dbContextApplication.Enrolls.Where(i => i.StudentId == CurStudent.Id).ToList();
                List<CourseDto> coursesDto = new List<CourseDto>();
                foreach (var favourite in favouraites)
                {
                    Course course = dbContextApplication.Courses.Where(i => i.Id == favourite.CourseId).FirstOrDefault();
                    CourseDto courseDto = new CourseDto();
                    courseDto.Title = course.Title;
                    courseDto.Description = course.Description; 
                    courseDto.CourseCode = course.CourseCode;   
                    courseDto.Id = course.Id;   
                    courseDto.Image = course.Image;    
                    courseDto.LevelId = course.LevelId; 
                    courseDto.DepartmentId = course.DepartmentId;
                    coursesDto.Add(courseDto);
                }
                List<Material> materials = dbContextApplication.Materials.Where(i => i.StudentId == CurStudent.Id).ToList();
                List<MaterialDto> materialsDto = new List<MaterialDto>();       
                foreach (var material in materials)
                {
                    MaterialDto materialDto = new MaterialDto();
                    materialDto.Description = material.Description;
                    materialDto.Path = material.Path;   
                    materialDto.Date = material.Date;
                    materialDto.StudentId = material.StudentId;
                    materialDto.CourseId = materialDto.CourseId;
                    materialsDto.Add(materialDto);  

                   
                }

                StudentProfileDto response = new StudentProfileDto();
                response.Bio = UserDetails.Bio;
                response.LeveL = UserDetails.Level;
                response.UserName = UserDetails.UserName;
                response.Department = UserDetails.Department;
                response.Materials = materialsDto;
                response.Courses = coursesDto;
                response.FavCount = coursesDto.Count();
                response.UploadCount = materialsDto.Count();

                // Serialize the response object
                var jsonResponse = JsonConvert.SerializeObject(response);

                return Ok(jsonResponse);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("ClickOnEnroll")]
        public async Task<IActionResult> ClickOnEnroll (string userId , int courseId) // click on favouriate icon to add or delete from list 
        {

            try
            {
                userId = DefaultUserId; // we will remove it later 
                Student CurStudnet = dbContextApplication.Students.Where(i => i.UserId == userId).FirstOrDefault();
                Enroll EnrollCourse = dbContextApplication.Enrolls.Where(i => i.StudentId == CurStudnet.Id && i.CourseId == courseId).FirstOrDefault();

                if (EnrollCourse is not null)
                {
                    dbContextApplication.Enrolls.Remove(EnrollCourse);
                    dbContextApplication.SaveChanges();
                    return Ok("course deleted from favouraite list");

                }
                else
                {
                    EnrollCourse = new Enroll();
                    EnrollCourse.CourseId = courseId;
                    EnrollCourse.StudentId = CurStudnet.Id;
                    dbContextApplication.Enrolls.Add(EnrollCourse);
                    dbContextApplication.SaveChanges();
                    return Ok("course added to favouriate list ");
                }



            }
            catch ( Exception ex ) { return BadRequest(ex.Message);
            
            }
                    
            


        }
        [HttpPut("EditProfile")]
        public async Task<IActionResult> EditProfile(string userId , int level , int department , string  bio)  // edit user details level + department +bio
        {
            try
            {
                // Access the DefaultUserId property
                userId = DefaultUserId; // will remove later .. 
                User UserDetails = dbContextApplication.Users.Where(i => i.Id == userId).FirstOrDefault();
                UserDetails.Level = level;
                UserDetails.Department = department;
                UserDetails.Bio = bio;
                dbContextApplication.SaveChanges();

                // Your action logic here
                return Ok(JsonConvert.SerializeObject(UserDetails));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
           
        }






    }
}
