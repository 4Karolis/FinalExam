using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Domain
{
    public class PersonalInfo
    {
        [ForeignKey("User")]
        public User User { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string PersonalCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Image ProfilePic { get; set; }
       
        [ForeignKey("ResidentialInfo")]
        public ResidentialInfo ResidentialInfo { get; set; }

        public PersonalInfo()
        {

        }
    }
}
