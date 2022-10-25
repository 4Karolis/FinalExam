using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Domain
{
    public class ResidentialInfo
    {
       
        public int Id { get; set; }
        [ForeignKey("PersonalInfo")]
        //public int PersonalInfoId { get; set; }
        public PersonalInfo PersonalInfo { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public int ApartmentNumber { get; set; }

        public ResidentialInfo()
        {

        }
    }
}
