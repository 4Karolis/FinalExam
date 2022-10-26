using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validation;

namespace DTOs
{
    public class ImageUploadDto
    {
        [MaxFileSize(20 * 1024 * 1024)]
        [AllowedExtensions(new[] {".png", ".jpg"})]
        public IFormFile ProfilePic { get; set; }
    }
}
