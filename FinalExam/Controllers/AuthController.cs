using Exam.BL;
using FinalExam.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FinalExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService _userService;
        private readonly IJwtService _jwtService;

        public AuthController(IUsersService usersService, IJwtService jwtService)
        {
            _userService = usersService;
            _jwtService = jwtService;
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> Signup(SignupDto signupDto)
        {
            var success = await _userService.CreateUserAsync(signupDto.Username, signupDto.Password, signupDto.PersonalInfo, signupDto.PersonalInfo.ResidentialInfo);

            return success ? Ok() : BadRequest(new { ErrorMessage = "User with this username already exists" });
        }
    }
}
