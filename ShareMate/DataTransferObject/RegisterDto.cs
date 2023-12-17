namespace ShareMate.DataTransferObject
{
    public class RegisterDto
    {
        public string username { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public int level { get; set; }
        public string bio { get; set; }
        public int department { get; set; }

        public string email { get; set; }   
    }
}
