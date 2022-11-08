using DTOs;
using Exam.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpPut("ChangeProfilePic")]
        [Authorize]
        public async Task<IActionResult> ChangeProfilePic(ImageUploadDto imageDto)
        {
            if (imageDto == null)
            {
                return BadRequest("Need to upload picture");
            }
            var userId = int.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);
            var imageBytes = await _imageService.GetImageBytesForProfilePicChangeAsync(imageDto);
            var contentType = imageDto.ProfilePic.ContentType;


            _imageService.ChangeProfilePicAsync(userId, imageBytes, contentType/*imageDto.ProfilePic.ContentType.ToString()*/);

            return Ok();
        }
    }
}
