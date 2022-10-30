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
        private readonly IPersonalInfoService _personalInfoService;
        private readonly IResidentialInfoService _residentialInfoService;

        public AuthController(IUsersService usersService, IJwtService jwtService, IImageService imageService,
            IPersonalInfoService personalInfoService, IResidentialInfoService residentialInfoService)
        {
            _userService = usersService;
            _jwtService = jwtService;
            _imageService = imageService;
            _personalInfoService = personalInfoService;
            _residentialInfoService = residentialInfoService;
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

            //var personalInfo = await _personalInfoService.GetPersonalInfoAsync(user.PersonalInfoId);
            //var residentialInfo = await _residentialInfoService.GetResidentialInfoAsync(personalInfo.ResidentialInfoId);

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
        [HttpGet("GetImageByIdTest")]
        public async Task<IActionResult> GetImage(int imageId)
        {
            var image =  await _imageService.GetImageAsync(imageId);
            return File(image.ImageBytes, image.ContentType);
        }
        [HttpPut("ChangeNAME")]
        [Authorize]
        public async Task<IActionResult> ChangeNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Info can't be null or whistespace");

            }
            var userId = int.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);
            await _personalInfoService.ChangeNameAsync(userId, name);
            return Ok();
        }
        [HttpPut("ChangeLastname")]
        [Authorize]
        public async Task<IActionResult> ChanegLastnameAsync(string lastname)
        {
            if (string.IsNullOrWhiteSpace(lastname))
            {
                return BadRequest("Info can't be null or whistespace");
            }
            var userId = int.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);
            await _personalInfoService.ChangeLastnameAsync(userId, lastname);
            return Ok();
        }
        [HttpPut("ChangePersonalCode")]
        [Authorize]
        public async Task<IActionResult> ChangePersonalCodeAsync(string personalCode)
        {
            if (string.IsNullOrWhiteSpace(personalCode))
            {
                return BadRequest("Info can't be null or whistespace");
            }
            var userId = int.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);
            await _personalInfoService.ChangePersonalCodeAsync(userId, personalCode);
            return Ok();
        }
        [HttpPut("ChangePhone")]
        [Authorize]
        public async Task<IActionResult> ChangePhoneAsync(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return BadRequest("Info can't be null or whistespace");
            }
            var userId = int.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);
            await _personalInfoService.ChangePhoneAsync(userId, phoneNumber);
            return Ok();

        }
    }
}
