using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Newtonsoft.Json;
using ShareMate.DataTransferObject;
using ShareMate.DbContext;
using ShareMate.Models;

namespace ShareMate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : BaseController
    {
        private readonly DbContextApplication dbContextApplication;

    
        public CourseController(DbContextApplication dbContextApplication)
        {
            this.dbContextApplication = dbContextApplication;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get( int  id ) {

            try
            {

                Course  course = dbContextApplication.Courses.Where(i => i.Id  == id).FirstOrDefault(); 
                
                    CourseDto courseDto = new CourseDto();
                    courseDto.Title = course.Title;
                    courseDto.Description = course.Description;
                    courseDto.DepartmentId = course.DepartmentId;
                    courseDto.LevelId = course.LevelId;
                    courseDto.CourseCode = course.CourseCode;
                    courseDto.Id = course.Id;
                    courseDto.Image = course.Image;     



                return Ok(JsonConvert.SerializeObject(courseDto));

            }
            catch (Exception ex) { 
             return BadRequest(ex.Message); 
            }
           
        
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search (string  title )
        {
            try
            { 
                List<Course> courses = dbContextApplication.Courses.Where(i => i.Title.Contains(title)).ToList();
                List<CourseDto> courseDtos = new List<CourseDto>();
                foreach (Course course in courses)
                {
                    CourseDto courseDto = new CourseDto();
                    courseDto.Title = course.Title;
                    courseDto.Description = course.Description;
                    courseDto.DepartmentId = course.DepartmentId;
                    courseDto.LevelId = course.LevelId;
                    courseDto.CourseCode = course.CourseCode;
                    courseDtos.Add(courseDto);

                }


                return Ok(JsonConvert.SerializeObject(courseDtos));

            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);      
            
            }



        }

        [HttpGet("FilterByLevel")]

        public async Task<IActionResult> FilterByLevel (int level)
        {
            List<Course> courses = dbContextApplication.Courses.Where(i => i.LevelId == level).ToList();
            List<CourseDto> courseDtos = new List<CourseDto>();
            foreach (Course course in courses)
            {
                CourseDto courseDto = new CourseDto();
                courseDto.Title=course.Title;
                courseDto.CourseCode=course.CourseCode;
                courseDto.DepartmentId = course.DepartmentId;
                courseDto.LevelId = course.LevelId;
                courseDto.Description=course.Description;   
                courseDto.Image = course.Image; 
                courseDto.Id = course.Id;   
                courseDtos.Add(courseDto);  

            }


            return Ok(JsonConvert.SerializeObject(courseDtos));
        }

        [HttpGet("FilterByDeptarment")]

        public async Task<IActionResult> FilterByDeptarment (int department)
        {
            List<Course> courses = dbContextApplication.Courses.Where(i => i.DepartmentId == department).ToList();
            List<CourseDto> courseDtos = new List<CourseDto>();
            foreach (Course course in courses)
            {
                CourseDto courseDto = new CourseDto();
                courseDto.Title = course.Title;
                courseDto.CourseCode = course.CourseCode;
                courseDto.DepartmentId = course.DepartmentId;
                courseDto.LevelId = course.LevelId;
                courseDto.Description = course.Description;
                courseDto.Image = course.Image;
                courseDto.Id = course.Id;
                courseDtos.Add(courseDto);

            }

            return Ok(JsonConvert.SerializeObject(courseDtos));
        }





    }
}
