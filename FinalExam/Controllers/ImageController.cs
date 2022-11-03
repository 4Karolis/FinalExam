using Exam.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalExam.Controllers
{
    public class ImageController : ControllerBase
    {       
        private readonly IImageService _imageService;        

        public ImageController(IUsersService usersService, IJwtService jwtService, IImageService imageService,
            IPersonalInfoService personalInfoService, IResidentialInfoService residentialInfoService)
        {            
            _imageService = imageService;
        }

        [HttpGet("GetImageByIdTest")]
        [Authorize]
        public async Task<IActionResult> GetImage(int imageId)
        {
            var image = await _imageService.GetImageAsync(imageId);
            return File(image.ImageBytes, image.ContentType);
        }
    }
}
