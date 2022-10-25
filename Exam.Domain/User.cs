using System;
using System.Collections.Generic;
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
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }
        // link to PersonalInfo

        public User()
        {

        }
    }
}
