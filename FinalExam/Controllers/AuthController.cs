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
        public async Task<IActionResult> Signup(SignupDto signupDto/*, [FromForm] ImageUploadDto imageDto*/)
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
        [HttpGet("GetUser")]
        [Authorize]
        public async Task<IActionResult> GetUserAsync()
        {
            var id = int.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }
        [HttpGet("GetUserInfo")]
        [Authorize]
        public async Task<IActionResult> GetUserInfo()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);
            var user = await _userService.GetUserByIdAsync(userId);
            var infoToReturn = new UserGetDto
            {
                Username = user.Username,
                Role = user.Role,
                Name = user.PersonalInfo.Name,
                Lastname = user.PersonalInfo.Lastname,
                PersonalCode = user.PersonalInfo.PersonalCode,
                Phone = user.PersonalInfo.Phone,
                Email = user.PersonalInfo.Email,
                City = user.PersonalInfo.ResidentialInfo.City,
                StreetName = user.PersonalInfo.ResidentialInfo.StreetName,
                HouseNumber = user.PersonalInfo.ResidentialInfo.HouseNumber.ToString(),
                ApartmentNumber = user.PersonalInfo.ResidentialInfo.ApartmentNumber.ToString()
            };
            return Ok(infoToReturn);
        }
    }
}
