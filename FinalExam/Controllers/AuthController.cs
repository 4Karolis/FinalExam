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
                HouseNumber = user.PersonalInfo.ResidentialInfo.HouseNumber,
                ApartmentNumber = user.PersonalInfo.ResidentialInfo.ApartmentNumber,
                ProfilePic = user.PersonalInfo.ProfilePic.ImageBytes
            };
            return Ok(infoToReturn);
        }
        [HttpDelete("DeleteUser")]
        [Authorize (Roles = "admin")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await _userService.DeleteUserAsync(userId);
            return Ok();
        }
        [HttpGet("GetImageByIdTest")]
        [Authorize]
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
        [HttpPut("ChangeEmail")]
        [Authorize]
        public async Task<IActionResult> ChangeEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Info can't be null or whistespace");
            }
            var userId = int.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);
            await _personalInfoService.ChangeEmailAsync(userId, email);
            return Ok();
        }
        [HttpPut("ChangeCity")]
        [Authorize]
        public async Task<IActionResult> ChangeCityAsync(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return BadRequest("Info can't be null or whistespace");
            }
            var userId = int.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);
            await _residentialInfoService.ChangeCityAsync(userId, city);
            return Ok();
        }
        [HttpPut("ChangeStreet")]
        [Authorize]
        public async Task<IActionResult> ChangeStreetAsync(string street)
        {
            if (string.IsNullOrWhiteSpace(street))
            {
                return BadRequest("Info can't be null or whistespace");
            }
            var userId = int.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);
            await _residentialInfoService.ChangeStreetAsync(userId, street);
            return Ok();
        }
        [HttpPut("ChangeHouseNumber")]
        [Authorize]
        public async Task<IActionResult> ChangeHouseNumberAsync(string houseNumber)
        {
            if (string.IsNullOrWhiteSpace(houseNumber))
            {
                return BadRequest("Info can't be null or whistespace");
            }
            var userId = int.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);
            await _residentialInfoService.ChangeHouseNumberAsync(userId, houseNumber);
            return Ok();
        }
        [HttpPut("ChangeApartmentNumber")]
        [Authorize]
        public async Task<IActionResult> ChangeApartmentNumberAsync(string apartmentNumber)
        {
            if (string.IsNullOrWhiteSpace(apartmentNumber))
            {
                return BadRequest("Info can't be null or whistespace");
            }
            var userId = int.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);
            await _residentialInfoService.ChangeApartmentNumberAsync(userId, apartmentNumber);
            return Ok();
        }
    }
}
