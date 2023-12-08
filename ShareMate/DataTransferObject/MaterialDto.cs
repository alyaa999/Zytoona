namespace ShareMate.DataTransferObject
{
    public class MaterialDto
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public DateTime Date { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
    }
}
