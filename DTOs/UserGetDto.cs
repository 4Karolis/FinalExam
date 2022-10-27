using FinalExam.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class UserGetDto
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string PersonalCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        //Image
        public string City { get; set; }
        public string StreetName { get;set; }
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
    }
}
