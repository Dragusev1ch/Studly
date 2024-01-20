using AutoMapper;
using Studly.BLL.DTO.Challenge;
using Studly.BLL.DTO.Customer;
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


    /// <summary>
    ///     List of exceptions
    /// </summary>
    private readonly ValidationException _alreadyExist = new("User with this email is already exist",
        "Please, use another email for registration");
    private readonly ValidationException _invalidUser = new("Current user was not found in our database",
        "It`s bug)");
    private readonly ValidationException _passwordDontMatch = new("The new and old passwords match",
        "Think of new password for your account");
    private readonly ValidationException _passwordNotCorrect = new("User name or password is not correct",
        "Check your email and password and try again");
    private readonly ValidationException _userNotFound = new("User name or password is not correct",
        "Check your email and password and try again");


    public CustomerService(IUnitOfWork uow, IMapper mapper, IPasswordHasher passwordHasher)
    {
        Database = uow;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    private IUnitOfWork Database { get; }


    public void Create(CustomerRegistrationDTO customerDto)
    {
        if (Database.Customers.GetAll().Any(u => u.Email == customerDto.Email))
            throw _alreadyExist;

        customerDto.Password = _passwordHasher.Hash(customerDto.Password);

        Database.Customers.Create(_mapper.Map<Customer>(customerDto));

        Database.Save();
    }

    public CustomerDto? Get(CustomerLoginDTO customerLoginDto)
    {
        var customer = Database.Customers.GetAll().FirstOrDefault(o => o.Email == customerLoginDto.Email);

        if (customer != null && !_passwordHasher.Verify(customer.Password, customerLoginDto.Password))
            throw _passwordNotCorrect;

        return _mapper.Map<CustomerDto>(customer);
    }

    public CustomerDto GetCurrent(string email)
    {
        var customer = Database.Customers.GetAll().FirstOrDefault(o => o.Email == email);

        return customer == null ? throw _userNotFound : _mapper.Map<CustomerDto>(customer);
    }

    public IQueryable<ChallengeDto> GetChallenges(string email)
    {
        var customer = Database.Customers.GetAll().FirstOrDefault(customer => customer.Email == email) ??
                       throw _invalidUser;

        var challenges = Database.Challenges.GetAll()
            .Where(o => o.CustomerId == customer.CustomerId)
            .Select(challenge => _mapper.Map<ChallengeDto>(challenge))
            .AsQueryable();

        return challenges;
    }

    public CustomerDto Update(CustomerPassUpdateDto newCustomerPass, string email)
    {
        if (newCustomerPass.OldPassword == newCustomerPass.NewPassword)
            throw _passwordDontMatch;

        var oldCustomer = Database.Customers.GetAll().FirstOrDefault(c => c.Email == email) ?? throw _invalidUser;

        oldCustomer.Password = _passwordHasher.Hash(newCustomerPass.NewPassword);

        Database.Save();

        return _mapper.Map<CustomerDto>(oldCustomer);
    }

    public CustomerDto Delete(string email)
    {
        var customer = Database.Customers.GetAll().FirstOrDefault(c => c.Email == email) ?? throw _invalidUser;

        Database.Customers.Delete(customer.CustomerId);
        Database.Save();
        return _mapper.Map<CustomerDto>(customer);
    }

    public void Dispose()
    {
        Database.Dispose();
    }
}