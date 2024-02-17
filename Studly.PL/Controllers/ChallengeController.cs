using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studly.BLL.DTO;
using Studly.BLL.DTO.Challenge;
using Studly.BLL.Infrastructure.Exceptions;
using Studly.BLL.Interfaces.Services;
using Studly.BLL.Services;
using Studly.DAL.Enums;

namespace Studly.PL.Controllers;

[ApiController]
[Route("api/challenge")]
public class ChallengeController : Controller
{
    private readonly IChallengeService _challengeService;
    private readonly ICustomerService _customerService;
    private readonly ILogger<ChallengeService> _logger;

    public ChallengeController(IChallengeService challengeService, ILogger<ChallengeService> logger,
        ICustomerService customerService)
    {
        _challengeService = challengeService;
        _logger = logger;
        _customerService = customerService;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult CreateChallenge(ChallengeRegistrationDto challenge)
    {
        if (challenge == null)
            throw new NullDataException("Challenge data is null",
                "Enter information for create challenge");

        var customerEmail = User.FindFirst(ClaimTypes.Email);

        if (customerEmail == null)
            throw new ValidationException("User with this email not found",
                "Check your email and try again");

        var res = _challengeService.Create(challenge, customerEmail.Value);

        return Ok(res);
    }

    [HttpGet]
    [Route("/api/challenges")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult GetUserChallenges([FromQuery] int offset, [FromQuery] int count,
        [FromQuery] bool? completedVisible, [FromQuery] bool? sortByPriority, [FromQuery] DateVariants? date,
        [FromQuery] ChallengeStatus? sortByStatus)
    {
        var customerEmail = User.FindFirst(ClaimTypes.Email);

        if (customerEmail == null)
            throw new ValidationException("User with this email not found",
                "Check your email and try again");

        var tasks = _challengeService.GetUserList(customerEmail.Value, offset, count, completedVisible, sortByPriority,
            date, sortByStatus);

        return Ok(tasks);
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult GetUserChallenges([FromQuery] int id)
    {
        var customerEmail = GetUserEmailFromToken();

        EnsureChallengeBelongToUser(customerEmail, id);

        var task = _challengeService.GetById(id);

        return Ok(task);
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult UpdateInfo([FromQuery] int id, [FromBody] ChallengeUpdateDto data)
    {
        var customerEmail = GetUserEmailFromToken();

        EnsureChallengeBelongToUser(customerEmail, id);

        var updated = _challengeService.Update(data, id);

        return Ok(updated);
    }

    [HttpDelete]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult Delete([FromQuery] int id)
    {
        var customerEmail = GetUserEmailFromToken();

        EnsureChallengeBelongToUser(customerEmail, id);

        var updated = _challengeService.Delete(id);

        return Ok(updated);
    }

    private void EnsureChallengeBelongToUser(string email, int challengeId)
    {
        if (_customerService.GetCurrent(email).Id == _challengeService.GetById(challengeId).CustomerId)
            throw new NotFoundException("Challenge do not found in users list",
                "Challenge id is wrong it's not belong to current user!");
    }

    private string GetUserEmailFromToken()
    {
        var customerEmail = User.FindFirst(ClaimTypes.Email);

        if (customerEmail == null)
            throw new ValidationException("User with this email not found",
                "Check your email and try again");

        return customerEmail.Value;
    }
}