using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studly.BLL.DTO;
using Studly.BLL.Infrastructure;
using Studly.BLL.Interfaces;
using Studly.PL.Models;

namespace Studly.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        [Route("api/[controller]/adminendpoint")]
        public IActionResult AdminsEndpoint()
        {
            var currentCustomer = GetCurrentCustomer();

            return Ok($"Hi {currentCustomer.Name}, you are an {currentCustomer.Role}");
        }


        [HttpGet]
        [Route("api/[controller]/public")]
        public IActionResult Public()
        {
            return Ok("Hi, you are on public property");
        }

        private CustomerViewModel GetCurrentCustomer()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var customerClaims = identity.Claims;

                return new CustomerViewModel
                {
                    Name = customerClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = customerClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    Role = customerClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value,
                };
            }
            return null;
        }
    }
}
