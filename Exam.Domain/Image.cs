using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Domain
{
    public class Image
    {
        public int Id { get; set; }
        //public string FileName { get; set; }
        [ForeignKey("PersonalInfo")]
        public int? PersonalInfoId { get; set; }
        public PersonalInfo? PersonalInfo { get; set; }
        public string ContentType { get; set; }
        public byte[] ImageBytes { get; set; }
        
        public Image()
        {

        }
    }
}
