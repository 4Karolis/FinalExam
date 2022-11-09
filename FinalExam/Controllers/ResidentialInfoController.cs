using Exam.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentialInfoController : ControllerBase
    {
        private readonly IResidentialInfoService _residentialInfoService;

        public ResidentialInfoController(IResidentialInfoService residentialInfoService)
        {
            _residentialInfoService = residentialInfoService;
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
