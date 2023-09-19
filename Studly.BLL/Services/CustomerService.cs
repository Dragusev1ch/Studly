using Studly.BLL.DTO;
using Studly.BLL.Infrastructure;
using Studly.BLL.Interfaces;
using Studly.Entities;
using Studly.Interfaces;

namespace Studly.BLL.Services;

public class CustomerService : ICustomerService
{
    private IUnitOfWork Database { get; set; }

    public CustomerService(IUnitOfWork uow)
    {
        Database = uow;
    }

    public void CreateCustomer(CustomerDTO customerDto)
    {
        Customer customer = new Customer()
        {
            CustomerId = customerDto.CustomerId,
            Name = customerDto.Name,
            Email = customerDto.Email,
            RegistrationDate = customerDto.RegistrationDate,
        };
        Database.Customers.Create(customer);
        Database.Save();
    }

    public CustomerDTO GetCustomerById(int? id)
    {
        if (id == null) throw new ValidationException("Id not set", "");

        var customer = Database.Customers.Get(id.Value);

        if (customer == null) throw new ValidationException("Customer not found", "");

        return new CustomerDTO
        {
            CustomerId = customer.CustomerId, Email = customer.Email, 
            Name = customer.Name, RegistrationDate = customer.RegistrationDate
        };
    }

    public IEnumerable<CustomerDTO> GetCustomers()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        Database.Dispose();
    }
}