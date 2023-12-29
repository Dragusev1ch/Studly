using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studly.BLL.DTO.Challenge;
using Studly.BLL.Infrastructure.Exceptions;
using Studly.BLL.Interfaces.Services;
using Studly.BLL.Services;
using System.Security.Claims;

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

            var customerEmail = User.FindFirst(ClaimTypes.Email);

            if (customerEmail == null) throw new ValidationException("User with this email not found",
                "Check your email and try again");

            _challengeService.CreateChallenge(challenge,customerEmail.Value);

            return Ok(_challengeService.GetChallenge(challenge.Title));
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
