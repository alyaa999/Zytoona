using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Newtonsoft.Json;
using ShareMate.DataTransferObject;
using ShareMate.DbContext;
using ShareMate.Models;
using System.Runtime.CompilerServices;

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
                    courseDto.title = course.Title;
                    courseDto.description = course.Description;
                    courseDto.departmentId = course.DepartmentId;
                    courseDto.levelId = course.LevelId;
                    courseDto.courseCode = course.CourseCode;
                    courseDto.id = course.Id;
                    courseDto.image = course.Image;     



                return Ok(JsonConvert.SerializeObject(courseDto));

            }
            catch (Exception ex) { 
             return BadRequest(ex.Message); 
            }
           
        
        }

        [HttpGet("CountMaterialOfCourse")]
        public async Task<IActionResult> CountMaterialsOfCourse(int id)
        {

            try
            {


                MaterialCountDto  materialCountDto = new MaterialCountDto();
                materialCountDto.slides = dbContextApplication.Materials.Where(i => i.CourseId == id && i.Type == 0).Count();
                materialCountDto.notes = dbContextApplication.Materials.Where(i => i.CourseId == id && i.Type == 1).Count();
                materialCountDto.practice = dbContextApplication.Materials.Where(i => i.CourseId == id && i.Type == 2).Count();
                materialCountDto.pastExam = dbContextApplication.Materials.Where(i => i.CourseId == id && i.Type == 3).Count();
                materialCountDto.links = dbContextApplication.Materials.Where(i => i.CourseId == id && i.Type == 4).Count();

                return Ok(JsonConvert.SerializeObject(materialCountDto));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {

            try
            {

                List<Course> courses = dbContextApplication.Courses.ToList();
                List<CourseDto> courseDtos = new List<CourseDto>(); 
                foreach (var course in  courses) {
                    CourseDto courseDto = new CourseDto();
                    courseDto.title = course.Title;
                    courseDto.description = course.Description;
                    courseDto.departmentId = course.DepartmentId;
                    courseDto.levelId = course.LevelId;
                    courseDto.courseCode = course.CourseCode;
                    courseDto.id = course.Id;
                    courseDto.image = course.Image;
                    courseDto.courseCode = course.CourseCode.ToString();    
                    courseDtos.Add(courseDto);  
                }


                return Ok(JsonConvert.SerializeObject(courseDtos));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search (string  title )
        {
            try
            {
                var courses = dbContextApplication.SearchCourses(title);

                List<CourseDto> courseDtos = new List<CourseDto>();
                foreach (Course course in courses)
                {
                    CourseDto courseDto = new CourseDto();
                    courseDto.title = course.Title;
                    courseDto.description = course.Description;
                    courseDto.departmentId = course.DepartmentId;
                    courseDto.levelId = course.LevelId;
                    courseDto.courseCode = course.CourseCode;
                    courseDto.image = course.Image;
                    courseDto.id = course.Id;
                    courseDtos.Add(courseDto);

                }


                return Ok(JsonConvert.SerializeObject(courseDtos));

            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);      
            
            }



        }

        [HttpGet("FilterByLevel")]

        public async  Task<IActionResult>  FilterByLevel (int level)
        {
            List<Course> courses = dbContextApplication.Courses.Where(i => i.LevelId == level).ToList();
            List<CourseDto> courseDtos = new List<CourseDto>();
            foreach (Course course in courses)
            {
                CourseDto courseDto = new CourseDto();
                courseDto.title=course.Title;
                courseDto.courseCode=course.CourseCode;
                courseDto.departmentId = course.DepartmentId;
                courseDto.levelId = course.LevelId;
                courseDto.description=course.Description;   
                courseDto.image = course.Image; 
                courseDto.id = course.Id;   
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
                courseDto.title = course.Title;
                courseDto.courseCode = course.CourseCode;
                courseDto.departmentId = course.DepartmentId;
                courseDto.levelId = course.LevelId;
                courseDto.description = course.Description;
                courseDto.image = course.Image;
                courseDto.id = course.Id;
                courseDtos.Add(courseDto);

            }

            return Ok(JsonConvert.SerializeObject(courseDtos));
        }





    }
}
