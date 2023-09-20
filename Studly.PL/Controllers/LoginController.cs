using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Studly.PL.Models;

namespace Studly.PL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public LoginController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login([FromBody] CustomerLogin userLogin)
    {
        var customer = Authenticate(userLogin);

        if (User != null)
        {
            var token = Generate(customer);
            return Ok(token);
        }

        return NotFound("Customer not found");
    }

    private string Generate(CustomerViewModel customer)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, customer.Name),
            new Claim(ClaimTypes.Email, customer.Email),
            new Claim(ClaimTypes.Role, customer.Role),
        };

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private CustomerViewModel Authenticate(CustomerLogin userLogin)
    {
        var currentCustomer = CustomerConstants.Customers.FirstOrDefault(o => 
            o.Name.ToLower() == userLogin.Name.ToLower() && o.Password == userLogin.Password);

        if (currentCustomer != null) return currentCustomer;

        return null;
    }
}