using Microsoft.AspNetCore.Mvc;
using Studly.BLL.DTO;
using Studly.BLL.Infrastructure;
using Studly.BLL.Interfaces;
using Studly.PL.Models;

namespace Studly.PL.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        private const string ControllerBaseRoute = "/api/customers";
        private ICustomerService _service { get; }

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }



        [HttpPost]
        public IActionResult CreateCustomer([Bind("CustomerId,Name,Email,RegistrationDate")] CustomerViewModel customer)
        {
            try
            {
                var customerDto = new CustomerDTO
                {
                    CustomerId = customer.CustomerId,
                    Name = customer.Name,
                    Email = customer.Email,
                    RegistrationDate = customer.RegistrationDate
                };
                _service.CreateCustomer(customerDto);
                return Content("Customer was created");
            }
            catch (ValidationException e)
            {
                ModelState.AddModelError(e.Property, e.Message);
            }
            return View(customer);
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            base.Dispose(disposing);
        }
    }
}
