using System.ComponentModel.DataAnnotations;

namespace ShareMate.Models
{
    public enum LeveL
    {
       level_1=1, level_2=2, level_3=3, level_4=4
    }

    public enum Department 
    {
       General = 1,ComputerScience=2 ,Information_Technology =3
    }
    public class Course
    {

        [Key]
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int DepartmentId { get; set; }
        public int LevelId { get; set; }    

        public List<Material> Materials { get; set; }
        public List<Enroll> Enrolls { get; set; }
    }
}
