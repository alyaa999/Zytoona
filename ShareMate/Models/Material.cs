using System.ComponentModel.DataAnnotations;

namespace ShareMate.Models
{
    public class Material
    {
        [Key]
        public int Id { get; set; }
        public int Type { get; set; }
        public DateTime Date { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
