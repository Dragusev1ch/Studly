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

    public void CreateCustomer(CustomerRegistrationDTO customerDto)
    {
        var similar = Database.Customers.GetAll().FirstOrDefault(o =>
            string.Equals(o.Email, customerDto.Email, StringComparison.OrdinalIgnoreCase));

        if (similar != null) throw new ValidationException("Customer with this email is exist", "");

        var customer = new Customer()
        {
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

    public CustomerDTO GetCurrentCustomer(string email)
    {
        var customer = Database.Customers.GetAll().FirstOrDefault(o =>
            string.Equals(o.Name, email, StringComparison.OrdinalIgnoreCase));

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

    public CustomerDTO Update(CustomerUpdateDTO newCustomer, string email)
    {
        if (string.Equals(newCustomer.OldPassword, newCustomer.NewPassword))
            throw new ValidationException("the new and old passwords match", "");

        var oldCustomer = Database.Customers.GetAll().FirstOrDefault(c => c.Email == email);


        if (oldCustomer != null)
        {
            oldCustomer.Password = newCustomer.NewPassword;
            Database.Save();
            return new CustomerDTO
            {
                CustomerId = oldCustomer.CustomerId,
                Email = oldCustomer.Email,
                Name = oldCustomer.Name,
                RegistrationDate = oldCustomer.RegistrationDate,
                Password = oldCustomer.Password
            };
        }
        
        throw new ValidationException("Customer not found", "");
    }

    public bool Delete(int id)
    {
        var customer = Database.Customers.Get(id);

        Database.Customers.Delete(customer.CustomerId);
        return true;
    }

    public bool DeleteCurrentCustomer(string customerName)
    {
        var customer = Database.Customers.GetAll().FirstOrDefault(o =>
            string.Equals(o.Name, customerName, StringComparison.OrdinalIgnoreCase));

        if (customer == null) return false;

        Database.Customers.Delete(customer.CustomerId);
        Database.Save();
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