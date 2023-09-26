using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Studly.BLL.DTO;
using Studly.BLL.DTO.Customer;
using Studly.BLL.Infrastructure;
using Studly.BLL.Interfaces;
using Studly.Entities;

namespace Studly.PL.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        [Route("api/customer")]
        [AllowAnonymous]
        public IActionResult CreateCustomer([FromBody] CustomerRegistrationDTO customer)
        {
            if (customer == null) throw new ValidationException("Customer data is null","");

            _customerService.CreateCustomer(customer);

            return Ok("Customer was created successfully");
        }

        [HttpGet]
        [Route("api/customer")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetCurrentCustomer()
        {
            var customerEmail = User.FindFirst(ClaimTypes.Email);
            
            if (customerEmail == null) throw new ValidationException("customer not found","");

            return Ok(_customerService.GetCurrentCustomer(customerEmail.Value));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("api/customers")]
        public IActionResult GetListOfCustomers()
        {
            return Ok(_customerService.List());
        }

        [HttpPut]
        [Route("api/customer")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult UpdateCustomer(CustomerUpdateDTO newCustomer)
        {
            var customerEmail = User.FindFirst(ClaimTypes.Email);

            if (customerEmail == null) throw new ValidationException("customer not found", "");

            return Ok(_customerService.Update(newCustomer, customerEmail.Value));
        }

        [HttpDelete]
        [Route("api/customer")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult DeleteCurrentCustomer()
        {
            var customerEmail = User.FindFirst(ClaimTypes.Email);

            if (customerEmail == null) throw new ValidationException("customer not found", "");

            return Ok(_customerService.DeleteCurrentCustomer(customerEmail.Value));
        }
    }
}
