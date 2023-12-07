namespace ShareMate.DataTransferObject
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int Level { get; set; }
        public string Bio { get; set; }
        public int Department { get; set; }

    }
}
