using DTOs;
using Exam.BL;
using FinalExam.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FinalExam.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> Signup(SignupDto signupDto)
        {
            var resizedImage = await _imageService.GetImageBytesAsync(signupDto);
            var savedImage = await _imageService.AddImageAsync(resizedImage, signupDto.PersonalInfo.ProfilePic.ContentType);
            var success = await _userService.CreateUserAsync(signupDto.Username, signupDto.Password, signupDto.PersonalInfo, signupDto.PersonalInfo.ResidentialInfo, savedImage);

            return success ? Ok() : BadRequest(new { ErrorMessage = "User with this username already exists" });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var (loginSuccess, user) = await _userService.LoginAsync(loginDto.Username, loginDto.Password);
            if (loginSuccess)
            {
                return Ok(new { Token = _jwtService.GetJwtToken(user) });
                { }
            }
            else
            {
                return BadRequest(new { ErrorMessage = "Login failed" });
            }
        }        
    }
}
