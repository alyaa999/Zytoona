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
        public async Task<IActionResult> ViewProfile(string username)  // view profile details + enroll courses  + upload materials + counts 
        {
            try
            {
                User UserDetails = dbContextApplication.Users.Where(i => i.UserName == username).FirstOrDefault();
                Student CurStudent = dbContextApplication.Students.Where(i => i.UserId ==UserDetails.Id).FirstOrDefault();
                List<Enroll> favouraites = dbContextApplication.Enrolls.Where(i => i.StudentId == CurStudent.Id).ToList();
                List<CourseDto> coursesDto = new List<CourseDto>();
                foreach (var favourite in favouraites)
                {
                    Course course = dbContextApplication.Courses.Where(i => i.Id == favourite.CourseId).FirstOrDefault();
                    CourseDto courseDto = new CourseDto();
                    courseDto.title = course.Title;
                    courseDto.description = course.Description; 
                    courseDto.courseCode = course.CourseCode;   
                    courseDto.id = course.Id;   
                    courseDto.image = course.Image;    
                    courseDto.levelId = course.LevelId; 
                    courseDto.departmentId = course.DepartmentId;
                    
                    coursesDto.Add(courseDto);
                }
                List<Material> materials = dbContextApplication.Materials.Where(i => i.StudentId == CurStudent.Id).ToList();
                List<MaterialDto> materialsDto = new List<MaterialDto>();       
                foreach (var material in materials)
                {
                    MaterialDto materialDto = new MaterialDto();
                    materialDto.description = material.Description;
                    materialDto.path = material.Path;   
                    materialDto.date = material.Date;
                    materialDto.type = material.Type;
                    materialDto.studentId = material.StudentId;
                    materialDto.courseId = material.CourseId;
                    materialDto.id = material.Id;
                    materialsDto.Add(materialDto);  

                   
                }

                StudentProfileDto response = new StudentProfileDto();
                response.bio = UserDetails?.Bio;
                response.leveL = UserDetails.Level;
                response.username = UserDetails.UserName;
                response.department = UserDetails.Department;
                response.materials = materialsDto;
                response.courses = coursesDto;
                response.favCount = coursesDto.Count();
                response.uploadCount = materialsDto.Count();
                response.email = UserDetails.Email;
               

                // Serialize the response object
                var jsonResponse = JsonConvert.SerializeObject(response);

                return Ok(jsonResponse);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("ClickOnEnroll")]
        public async Task<IActionResult> ClickOnEnroll (string userName , int courseId) // click on favouriate icon to add or delete from list 
        {

            try
            {
                string userId = dbContextApplication.Users.Where(i => i.UserName == userName).Select(i => i.Id).FirstOrDefault();
                Student CurStudnet = dbContextApplication.Students.Where(i => i.UserId == userId).FirstOrDefault();
                Enroll EnrollCourse = dbContextApplication.Enrolls.Where(i => i.StudentId == CurStudnet.Id && i.CourseId == courseId).FirstOrDefault();

                if (EnrollCourse is not null)
                {
                    dbContextApplication.Enrolls.Remove(EnrollCourse);
                    dbContextApplication.SaveChanges();
                    return Ok(false);

                }
                else
                {
                    EnrollCourse = new Enroll();
                    EnrollCourse.CourseId = courseId;
                    EnrollCourse.StudentId = CurStudnet.Id;
                    dbContextApplication.Enrolls.Add(EnrollCourse);
                    dbContextApplication.SaveChanges();
                    return Ok(true);
                }



            }
            catch ( Exception ex ) { return BadRequest(ex.Message);
            
            }
                    
            


        }

        [HttpGet("IsFavourite")]
        public async Task<IActionResult> IsFavourite(string userName, int courseId) // click on favouriate icon to add or delete from list 
        {

            try
            {
                string userId = dbContextApplication.Users.Where(i => i.UserName == userName).Select(i => i.Id).FirstOrDefault();
                Student CurStudnet = dbContextApplication.Students.Where(i => i.UserId == userId).FirstOrDefault();
                Enroll EnrollCourse = dbContextApplication.Enrolls.Where(i => i.StudentId == CurStudnet.Id && i.CourseId == courseId).FirstOrDefault();

                if (EnrollCourse is not null)
                {
                    
                    return Ok(true);

                }
                else
                {
                   
                    return Ok(false);
                }



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

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
