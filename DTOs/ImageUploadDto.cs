using Microsoft.AspNetCore.Http;
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
