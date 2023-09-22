using Studly.BLL.DTO.Customer;
using Studly.BLL.Infrastructure;
using Studly.BLL.Interfaces;
using Studly.Entities;
using Studly.Interfaces;
using Studly.Repositories;

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
        var customer = new Customer()
        {
            //TODO auto fill id property 
            //Database.Customers.GetAll().Any() ? Database.Customers.GetAll().Max(x => x.CustomerId) + 1 : 1,
            CustomerId = customerDto.CustomerId, 
            Name = customerDto.Name,
            Email = customerDto.Email,
            Password = customerDto.Password,
            RegistrationDate = DateTime.Now
        };
        Database.Customers.Create(customer);
        Database.Save();
    }

    public CustomerDTO GetCustomer(CustomerLoginDTO customerLoginDto)
    {
        var customer = Database.Customers.GetAll().FirstOrDefault(o =>
            string.Equals(o.Email, customerLoginDto.Email, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(o.Password, customerLoginDto.Password));

        if (customer != null)
            return new CustomerDTO
            {
                CustomerId = customer.CustomerId,
                Email = customer.Email,
                Name = customer.Name,
                RegistrationDate = customer.RegistrationDate,
                Password = customer.Password
            };
        throw new ValidationException("Customer not found", "");
    }

    public IEnumerable<CustomerDTO> List()
    {
        return Database.Customers.GetAll().Select(customer => new CustomerDTO
        {
            CustomerId = customer.CustomerId,
            Email = customer.Email,
            Name = customer.Name,
            RegistrationDate = customer.RegistrationDate,
            Password = customer.Password
        });
    }

    public CustomerDTO Update(CustomerDTO newCustomer)
    {
        var oldCustomer = Database.Customers.Get(newCustomer.CustomerId);

        if (oldCustomer != null)
        {
            oldCustomer.Name = newCustomer.Name;
            oldCustomer.Email = newCustomer.Email;
            oldCustomer.RegistrationDate = newCustomer.RegistrationDate;
            oldCustomer.Password = newCustomer.Password;

            return newCustomer;
        }

        throw new ValidationException("Customer not found", "");
    }

    public bool Delete(int id)
    {
        var customer = Database.Customers.Get(id);

        if(customer == null) return false;

        Database.Customers.Delete(customer.CustomerId);
        return true;
    }

    public CustomerDTO GetCustomerById(int? id)
    {
        if (id == null) throw new ValidationException("Id not set", "");

        var customer = Database.Customers.Get(id.Value);

        if (customer == null) throw new ValidationException("Customer not found", "");

        return new CustomerDTO
        {
            CustomerId = customer.CustomerId,
            Email = customer.Email,
            Name = customer.Name,
            RegistrationDate = customer.RegistrationDate,
            Password = customer.Password
        };
    }

    public void Dispose()
    {
        Database.Dispose();
    }
}