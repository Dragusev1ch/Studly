using AutoMapper;
using Studly.BLL.DTO.Customer;
using Studly.BLL.Infrastructure;
using Studly.BLL.Interfaces;
using Studly.BLL.Interfaces.Services;
using Studly.Entities;
using Studly.Interfaces;
using Studly.Repositories;

namespace Studly.BLL.Services;

public class CustomerService : ICustomerService
{
    private IUnitOfWork Database { get; set; }
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    public CustomerService(IUnitOfWork uow,IMapper mapper,IPasswordHasher passwordHasher)
    {
        Database = uow;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public void CreateCustomer(CustomerRegistrationDTO customerDto)
    {
        var similar = Database.Customers.GetAll().FirstOrDefault(o => o.Email == customerDto.Email);

        customerDto.Password = _passwordHasher.Hash(customerDto.Password);

        if (similar != null) throw new ValidationException("Customer with this email is exist", "");

        Database.Customers.Create(_mapper.Map<Customer>(customerDto));

        Database.Save();
    }

    public CustomerDTO? GetCustomer(CustomerLoginDTO customerLoginDto)
    {
        var customer = Database.Customers.GetAll().FirstOrDefault(o => o.Email == customerLoginDto.Email);

        if (customer != null && !_passwordHasher.Verify(customer.Password, customerLoginDto.Password))
            throw new ValidationException("User name of password is not correct", "");

        return _mapper.Map<CustomerDTO>(customer);
    }

    public CustomerDTO GetCurrentCustomer(string email)
    {
        var customer = Database.Customers.GetAll().FirstOrDefault(o => o.Email == email);

        if (customer != null) return _mapper.Map<CustomerDTO>(customer);

        throw new ValidationException("Customer not found", "");
    }

    public IQueryable<CustomerDTO> List()
    {
        return Database.Customers.GetAll().Select(customer => _mapper.Map<CustomerDTO>(customer));
    }

    public CustomerDTO Update(CustomerUpdateDTO newCustomer, string email)
    {
        if (newCustomer.OldPassword == newCustomer.NewPassword)
            throw new ValidationException("the new and old passwords match", "");

        var oldCustomer = Database.Customers.GetAll().FirstOrDefault(c => c.Email == email);


        if (oldCustomer != null)
        {
            oldCustomer.Password = newCustomer.NewPassword;

            Database.Save();

            return _mapper.Map<CustomerDTO>(oldCustomer);
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

        return _mapper.Map<CustomerDTO>(customer);
    }

    public void Dispose()
    {
        Database.Dispose();
    }
}