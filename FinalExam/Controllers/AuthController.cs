using DTOs;
using Exam.BL;
using Exam.Domain;
using FinalExam.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<IActionResult> Signup(SignupDto signupDto)
        {            

            using var memoryStream = new MemoryStream();
            signupDto.PersonalInfo.ProfilePic.CopyTo(memoryStream);
            var imageBytes = memoryStream.ToArray();

            var savedImage = await _imageService.AddImageAsync(imageBytes, signupDto.PersonalInfo.ProfilePic.ContentType);
            

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
