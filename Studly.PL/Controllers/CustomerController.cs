using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studly.BLL.DTO.Customer;
using Studly.BLL.Infrastructure.Exceptions;
using Studly.BLL.Interfaces.Services;
using Studly.BLL.Services;

namespace Studly.PL.Controllers;

[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly ILogger<CustomerService> _logger;

    public CustomerController(ICustomerService customerService, ILogger<CustomerService> logger)
    {
        _customerService = customerService;
        _logger = logger;
    }

    [HttpPost]
    [Route("api/customer")]
    [AllowAnonymous]
    public IActionResult CreateCustomer([FromBody] CustomerRegistrationDTO customer)
    {
            if (customer == null) throw new NullDataException("User data is null",
                "Enter information for create account");

            _customerService.CreateCustomer(customer);

            return Ok(_customerService.GetCurrentCustomer(customer.Email));
    }

    [HttpGet]
    [Route("api/customer")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult GetCurrentCustomer()
    {
        var customerEmail = User.FindFirst(ClaimTypes.Email);

        if (customerEmail == null) throw new ValidationException("User with this email not found",
            "Check your email and try again");

        return Ok(_customerService.GetCurrentCustomer(customerEmail.Value));
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/customers")]
    public IActionResult GetListOfCustomers()
    {
        return Ok(_customerService.List());
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/my_challenges")]
    public IActionResult GetListOfChallenges()
    {
        var customerEmail = User.FindFirst(ClaimTypes.Email);

        if (customerEmail == null) throw new ValidationException("User with this email not found",
            "Check your email and try again");

        var tasks = _customerService.GetCustomerChallenges(customerEmail.Value);

        if (tasks == null) return Ok("Your collection of tasks is still empty");

        return Ok(tasks);
    }

    [HttpPut]
    [Route("api/customer")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult UpdateCustomer(CustomerUpdateDTO newCustomer)
    {
        var customerEmail = User.FindFirst(ClaimTypes.Email);

        if (customerEmail == null) throw new ValidationException("User with this email not found",
            "Check your email and try again");

        return Ok(_customerService.Update(newCustomer, customerEmail.Value));
    }

    [HttpDelete]
    [Route("api/customer")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult DeleteCurrentCustomer()
    {
        var customerEmail = User.FindFirst(ClaimTypes.Email);

        if (customerEmail == null) throw new ValidationException("User with this email not found",
            "Check your email and try again");

        return Ok(_customerService.DeleteCurrentCustomer(customerEmail.Value));
    }
}