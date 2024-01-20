﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studly.BLL.DTO.Customer;
using Studly.BLL.Infrastructure.Exceptions;
using Studly.BLL.Interfaces.Services;
using Studly.BLL.Services;

namespace Studly.PL.Controllers;

[ApiController]
[Route("api")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly IChallengeService _challengeService;
    private readonly ILogger<CustomerService> _logger;

    public CustomerController(ICustomerService customerService, ILogger<CustomerService> logger, IChallengeService challengeService)
    {
        _customerService = customerService;
        _logger = logger;
        _challengeService = challengeService;
    }

    [HttpPost]
    [Route("customer")]
    [AllowAnonymous]
    public IActionResult Create([FromBody] CustomerRegistrationDTO customer)
    {
        if (customer == null)
            throw new NullDataException("User data is null",
                "Enter information for create account");

        _customerService.Create(customer);

        return Ok(_customerService.GetCurrent(customer.Email));
    }

    [HttpGet]
    [Route("customer")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult GetCurrent()
    {
        var customerEmail = User.FindFirst(ClaimTypes.Email);

        if (customerEmail == null)
            throw new ValidationException("User with this email not found",
                "Check your email and try again");

        return Ok(_customerService.GetCurrent(customerEmail.Value));
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("my-challenges")]
    public IActionResult GetAllUserChallenges()
    {
        var customerEmail = User.FindFirst(ClaimTypes.Email);

        if (customerEmail == null)
            throw new ValidationException("User with this email not found",
                "Check your email and try again");

        var tasks = _challengeService.GetUserList(customerEmail.Value);

        if (tasks == null) return Ok("Your collection of tasks is still empty");

        return Ok(tasks);
    }

    [HttpPut]
    [Route("customer")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult UpdatePassword(CustomerPassUpdateDto newCustomerPass)
    {
        var customerEmail = User.FindFirst(ClaimTypes.Email);

        if (customerEmail == null)
            throw new ValidationException("User with this email not found",
                "Check your email and try again");

        return Ok(_customerService.Update(newCustomerPass, customerEmail.Value));
    }

    [HttpDelete]
    [Route("customer")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult DeleteCurrent()
    {
        var customerEmail = User.FindFirst(ClaimTypes.Email);

        if (customerEmail == null)
            throw new ValidationException("User with this email not found",
                "Check your email and try again");

        return Ok(_customerService.Delete(customerEmail.Value));
    }
}