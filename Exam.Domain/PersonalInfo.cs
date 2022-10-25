using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Domain
{
    public class PersonalInfo
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string PersonalCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Image ProfilePic { get; set; }
        //Link to ResidentialInfo

        public PersonalInfo()
        {

        }
    }
}
