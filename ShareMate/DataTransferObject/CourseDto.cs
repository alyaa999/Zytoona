namespace ShareMate.DataTransferObject
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int DepartmentId { get; set; }
        public int LevelId { get; set; }
    }
}
