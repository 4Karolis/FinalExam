using DTOs;
using Exam.BL;
using FinalExam.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FinalExam.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService _userService;
        private readonly IJwtService _jwtService;
        private readonly IImageService _imageService;

        public AuthController(IUsersService usersService, IJwtService jwtService, IImageService imageService)
        {
            _userService = usersService;
            _jwtService = jwtService;
            _imageService = imageService;
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> Signup(SignupDto signupDto, [FromForm] ImageUploadDto imageDto)
        {            

            using var memoryStream = new MemoryStream();
            imageDto.ProfilePic.CopyTo(memoryStream);
            var imageBytes = memoryStream.ToArray();

            var savedImage = await _imageService.AddImageAsync(imageBytes, imageDto.ProfilePic.ContentType);
            

            var success = await _userService.CreateUserAsync(signupDto.Username, signupDto.Password, signupDto.PersonalInfo, signupDto.PersonalInfo.ResidentialInfo, savedImage);

            return success ? Ok() : BadRequest(new { ErrorMessage = "User with this username already exists" });
        }
    }
}
