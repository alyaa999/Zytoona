namespace ShareMate.DataTransferObject
{
    public class MaterialDto
    {
        public int id { get; set; }
        public int type { get; set; }
        public DateTime date { get; set; }
        public string path { get; set; }
        public string description { get; set; }
        public int courseId { get; set; }
        public int studentId { get; set; }
    }
}
