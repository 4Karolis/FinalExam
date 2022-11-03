using DTOs;
using Exam.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalExam.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUsersService _userService;
       
        public UserController(IUsersService usersService)
        {
            _userService = usersService;           
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await _userService.DeleteUserAsync(userId);
            return Ok();
        }
    }
}
