using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studly.BLL.DTO;
using Studly.BLL.DTO.Customer;
using Studly.BLL.Infrastructure;
using Studly.BLL.Interfaces;
using Studly.Entities;
using Studly.PL.Models;

namespace Studly.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/[controller]/createCustomer")]
        public IActionResult CreateCustomer([FromBody] CustomerDTO customer)
        {
            if (customer == null) throw new ValidationException("Customer data is null","");

            _customerService.CreateCustomer(customer);

            return Ok("Customer was created successfully");
        }

        [HttpGet]
        [Route("api/[controller]/getCustomer")]
        public IActionResult GetCustomer(int id)
        {
            if (id <= 0) throw new ValidationException("Invalid customer Id", "");

            var customer = _customerService.GetCustomerById(id);

            if (customer == null) throw new ValidationException("Customer not found", "");

            return Ok(customer);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        [Route("api/[controller]/getListOfCustomers")]
        public IActionResult GetListOfCustomers()
        {
            return Ok(_customerService.List());
        }

        [HttpPut]
        [Route("api/[controller]/updateCustomer")]
        public IActionResult UpdateCustomer(CustomerDTO newCustomer)
        {
            if (newCustomer == null) throw new ValidationException("new customer is incorrect", "");

            _customerService.Update(newCustomer);

            return Ok("Customer updated successfully");
        }

        [HttpDelete]
        [Route("api/[controller]/deleteCustomer")]
        public IActionResult DeleteCustomer(int id)
        {
            if(id <= 0) throw new ValidationException("Invalid customer Id", "");

            _customerService.Delete(id);

            return Ok("Customer deleted successfully");
        }

        [HttpGet]
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
