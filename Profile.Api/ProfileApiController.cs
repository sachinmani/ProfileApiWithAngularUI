using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProfileApi.BusinessService;

namespace Profile.Api
{
    [ApiController]
    public class ProfileApiController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileApiController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        [Route("/")]
        public async Task<IActionResult> GetAll()
        {
            var profiles = await _profileService.GetAll();
            return Ok(profiles);
        }

        [HttpPost("/")]
        public async Task<IActionResult> Post(ProfileApi.ViewModels.Profile profile)
        {
            
            if (profile == null || string.IsNullOrWhiteSpace(profile.Firstname) ||
                string.IsNullOrWhiteSpace(profile.Lastname))
            {
                return BadRequest("Either firstname or lastname is empty");
            }
            await _profileService.AddProfile(profile);
            return Ok();
        }
    }
}