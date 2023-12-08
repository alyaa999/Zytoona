using System.ComponentModel.DataAnnotations;

namespace ShareMate.Models
{
    public class Student 
    {
        [Key]
        public int Id { get; set; }  

        public string UserId { get; set; }     
         public User User { get; set; }     

        public List<Material> Materials { get; set; }
        public List<Enroll> Enrolls { get; set; }
    }
}
