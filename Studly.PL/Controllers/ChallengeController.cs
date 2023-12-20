using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studly.BLL.DTO.Challenge;
using Studly.BLL.Infrastructure.Exceptions;
using Studly.BLL.Interfaces.Services;
using Studly.BLL.Services;

namespace Studly.PL.Controllers
{
    [ApiController]
    public class ChallengeController : Controller
    {
        private readonly IChallengeService _challengeService;
        private readonly ILogger<ChallengeService> _logger;

        public ChallengeController(IChallengeService challengeService, ILogger<ChallengeService> logger)
        {
            _challengeService = challengeService;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/challenge")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult CreateChallenge(ChallengeRegistrationDto challenge)
        {
            if (challenge == null) throw new NullDataException("Challenge data is null",
                    "Enter information for create challenge");

            _challengeService.CreateChallenge(challenge);

            return Ok();
        }

        [HttpGet]
        [Route("api/challenge")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetListOfChallenges()
        {
            return Ok(_challengeService.List());
        }
    }
}
