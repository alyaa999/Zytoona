using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareMate.Models;

namespace ShareMate.DataTransferObject
{

    public class StudentProfileDto 
    {
        public int Id { get; set; }
        public string UserName { get; set; }    

        public int LeveL {get; set; }   
        public string? Bio { get; set; }    
        public int Department { get; set; }     

        public List<MaterialDto> Materials { get; set; }
        public List<CourseDto> Courses { get; set; }  

        public int FavCount { get; set; }

        public int  UploadCount { get; set; }   

    }
}
