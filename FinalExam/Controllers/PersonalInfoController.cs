using Exam.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalInfoController : ControllerBase
    {        
        private readonly IPersonalInfoService _personalInfoService;

        public PersonalInfoController(IPersonalInfoService personalInfoService)
        {
            _personalInfoService = personalInfoService;
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

        //ADD ChangeProfilePic
    }
}
