using Studly.BLL.DTO;
using Studly.BLL.Infrastructure;
using Studly.BLL.Interfaces;
using Studly.Interfaces;

namespace Studly.BLL.Services;

public class CustomerServices : ICustomerService
{
    private IUnitOfWork Database { get; set; }

    public CustomerServices(IUnitOfWork uow)
    {
        Database = uow;
    }

    public void CreateCustomer(CustomerDTO customerDto)
    {
        var customer = Database.Customers.Get(customerDto.CustomerId);

        if (customer == null) throw new ValidationException("Customer not found","");

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