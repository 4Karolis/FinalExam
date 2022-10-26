using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }
        //[ForeignKey("PersonalInfo")]
        //public int PersonalInfoId { get; set; }
        public PersonalInfo PersonalInfo { get; set; } 

        // link to PersonalInfo

        public User()
        {

        }
    }
}
