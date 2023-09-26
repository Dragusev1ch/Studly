using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Studly.BLL.DTO.Customer;
using Studly.BLL.Infrastructure;
using Studly.BLL.Interfaces;

namespace Studly.PL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly IConfiguration _configuration;
    public LoginController(ICustomerService customerService,IConfiguration configuration)
    {
        _customerService = customerService;
        _configuration = configuration;
    }

    
    [HttpPost]
    [AllowAnonymous]
    [Route("/login")]
    public IActionResult Login([FromBody] CustomerLoginDTO customer)
    {
        if (string.IsNullOrEmpty(customer.Email) || string.IsNullOrEmpty(customer.Password))
            throw new ValidationException("Login failed", "");

        var loggerInUser = _customerService.GetCustomer(customer);

        return loggerInUser is null ? throw new ValidationException("User not found", "") : Ok(Generate(loggerInUser));
    }

    private string Generate(CustomerDTO customer)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, customer.Name),
            new Claim(ClaimTypes.Email, customer.Email)
        };

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}