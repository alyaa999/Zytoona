namespace ShareMate.DataTransferObject
{
    public class CourseDto
    {
        public int id { get; set; }
        public string courseCode { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public string title { get; set; }
        public int departmentId { get; set; }
        public int levelId { get; set; }
    }
}
