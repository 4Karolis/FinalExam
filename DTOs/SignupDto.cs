using FinalExam.DTOs;

namespace FinalExam.DTOs
{
    public class SignupDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public PersonalInfoDto PersonalInfo { get; set; }
    }
}
