using Microsoft.AspNetCore.Http;

namespace FinalExam.DTOs
{
    public class PersonalInfoDto
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string PersonalCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public IFormFile ProfilePic { get; set; }
        public ResidentialInfoDto ResidentialInfo { get; set; }
    }
}
