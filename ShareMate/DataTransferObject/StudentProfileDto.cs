using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareMate.Models;

namespace ShareMate.DataTransferObject
{

    public class StudentProfileDto 
    {
        public string username { get; set; }    

        public int leveL {get; set; }   
        public string? bio { get; set; }    
        public int department { get; set; }     

        public List<MaterialDto> materials { get; set; }
        public List<CourseDto> courses { get; set; }  

        public int favCount { get; set; }

        public int  uploadCount { get; set; }   

        public string email { get; set; }   

    }
}
