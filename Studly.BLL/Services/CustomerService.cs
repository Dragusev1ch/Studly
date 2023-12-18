using AutoMapper;
using Microsoft.Extensions.Logging;
using Studly.BLL.DTO.Customer;
using Studly.BLL.Infrastructure;
using Studly.BLL.Infrastructure.Exceptions;
using Studly.BLL.Interfaces;
using Studly.BLL.Interfaces.Services;
using Studly.Entities;
using Studly.Interfaces;

namespace Studly.BLL.Services;

public class CustomerService : ICustomerService
{
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    public CustomerService(IUnitOfWork uow, IMapper mapper, IPasswordHasher passwordHasher)
    {
        Database = uow;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    private IUnitOfWork Database { get; }

    public void CreateCustomer(CustomerRegistrationDTO customerDto)
    {
            if (Database.Customers.GetAll().Any(u => u.Email == customerDto.Email))
                throw new ValidationException("User with this email is already exist",
                    "Please, use another email for registration");

            customerDto.Password = _passwordHasher.Hash(customerDto.Password);

            Database.Customers.Create(_mapper.Map<Customer>(customerDto));

            Database.Save();
    }

    public CustomerDTO? GetCustomer(CustomerLoginDTO customerLoginDto)
    {
        var customer = Database.Customers.GetAll().FirstOrDefault(o => o.Email == customerLoginDto.Email);

        if (customer != null && !_passwordHasher.Verify(customer.Password, customerLoginDto.Password))
            throw new ValidationException("User name or password is not correct",
                "Check your email and password and try again");

        return _mapper.Map<CustomerDTO>(customer);
    }

    public CustomerDTO GetCurrentCustomer(string email)
    {
        var customer = Database.Customers.GetAll().FirstOrDefault(o => o.Email == email);

        if (customer != null) return _mapper.Map<CustomerDTO>(customer);

        throw new NotFoundException("User with this email not found",
            "Check your email and try again");
    }

    public IQueryable<CustomerDTO> List()
    {
        return Database.Customers.GetAll().Select(customer => _mapper.Map<CustomerDTO>(customer));
    }

    public CustomerDTO Update(CustomerUpdateDTO newCustomer, string email)
    {
        if (newCustomer.OldPassword == newCustomer.NewPassword)
            throw new ValidationException("The new and old passwords match",
                "Think of new password for your account");

        var oldCustomer = Database.Customers.GetAll().FirstOrDefault(c => c.Email == email);


        if (oldCustomer != null)
        {
            oldCustomer.Password = _passwordHasher.Hash(newCustomer.NewPassword);

            Database.Save();

            return _mapper.Map<CustomerDTO>(oldCustomer);
        }

        throw new ValidationException("User with this data is not found",
            "Check your email and try again");
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
        if (id == null) throw new NullDataException("Id not set", "Unlucky");

        var customer = Database.Customers.Get(id.Value);

        if (customer == null) throw new ValidationException("Customer with this Id not found", 
            "Check Id and try again");

        return _mapper.Map<CustomerDTO>(customer);
    }

    public void Dispose()
    {
        Database.Dispose();
    }
}