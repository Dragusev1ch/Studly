using AutoMapper;
using Studly.BLL.DTO.Customer;
using Studly.BLL.Infrastructure;
using Studly.BLL.Interfaces;
using Studly.BLL.Interfaces.Services;
using Studly.Entities;
using System.Xml.Linq;

namespace Studly.BLL.Services;

public class CustomerService : ICustomerService
{
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDatabaseService _databaseService;


    public CustomerService(IMapper mapper, IPasswordHasher passwordHasher,IDatabaseService databaseService)
    {
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _databaseService = databaseService;
    }

    public async Task CreateCustomer(CustomerRegistrationDTO customerDto)
    {
        customerDto.Password = _passwordHasher.Hash(customerDto.Password);

        await _databaseService.SaveEntityAsync(_mapper.Map<Customer>(customerDto));
    }

    public async Task<CustomerDTO> GetCustomer(CustomerLoginDTO customerLoginDto)
    { 
        var l =  await _databaseService.GetEntitiesAsync<Customer>();

       var customer = l.FirstOrDefault(o => o.Email == customerLoginDto.Email);

        if (customer != null && !_passwordHasher.Verify(customer.Password, customerLoginDto.Password))
            throw new ValidationException("User name of password is not correct", "");

        return _mapper.Map<CustomerDTO>(customer);
    }

    public async Task<CustomerDTO> GetCurrentCustomer(string email)
    {
        var l = await _databaseService.GetEntitiesAsync<Customer>();

        var customer = l.FirstOrDefault(o => o.Email == email);

        if (customer != null) return _mapper.Map<CustomerDTO>(customer);

        throw new ValidationException("Customer not found", "");
    }

    public async Task<IEnumerable<CustomerDTO>> List()
    {
        var list = await _databaseService.GetEntitiesAsync<Customer>();

        return list.Select(customer => _mapper.Map<CustomerDTO>(customer));
    }

    public async Task<CustomerDTO> Update(CustomerUpdateDTO newCustomer, string email)
    {
        if (newCustomer.OldPassword == newCustomer.NewPassword)
            throw new ValidationException("the new and old passwords match", "");

        var list = await _databaseService.GetEntitiesAsync<Customer>();

        var oldCustomer = list.FirstOrDefault(c => c.Email == email);

        if (oldCustomer != null)
        {
            oldCustomer.Password = _passwordHasher.Hash(newCustomer.NewPassword);

            await _databaseService.SaveEntityAsync(oldCustomer);

            return _mapper.Map<CustomerDTO>(oldCustomer);
        }

        throw new ValidationException("Customer not found", "");
    }

    public async Task<int> Delete(int id)
    {
        var customer = await _databaseService.GetEntityAsync<Customer>(id);

        return await _databaseService.DeleteEntityAsync<Customer>(customer);
    }

    public async Task<int> DeleteCurrentCustomer(string customerName)
    {
        var list = await _databaseService.GetEntitiesAsync<Customer>();

        var customer = list.FirstOrDefault(o => o.Name == customerName);

        if (customer == null) throw new ValidationException("customer not found", "");

        return await _databaseService.DeleteEntityAsync<Customer>(customer);
    }

    public async Task<CustomerDTO> GetCustomerById(int id)
    {
        if (id == null) throw new ValidationException("Id not set", "");

        var customer = await _databaseService.GetEntityAsync<Customer>(id);

        if (customer == null) throw new ValidationException("Customer not found", "");

        return _mapper.Map<CustomerDTO>(customer);
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}