﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studly.BLL.DTO.Challenge;
using Studly.BLL.Infrastructure.Exceptions;
using Studly.BLL.Interfaces.Services;
using Studly.BLL.Services;

namespace Studly.PL.Controllers; 

[ApiController]
[Route("api")]
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
    [Route("challenge")]
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
}