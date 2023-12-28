using Studly.BLL.DTO.Challenge;
using Studly.BLL.DTO.Customer;

namespace Studly.BLL.Interfaces.Services;

public interface ICustomerService
{
    public void CreateCustomer(CustomerRegistrationDTO customerDto);
    public CustomerDto? GetCustomer(CustomerLoginDTO  customerLoginDto);
    public CustomerDto GetCurrentCustomer(string email);
    public IQueryable<CustomerListDto> List();
    public IQueryable<ChallengeDto>? GetCustomerChallenges(string email);
    public CustomerDto Update(CustomerUpdateDTO newCustomer, string email);
    public bool Delete(int id);
    public bool DeleteCurrentCustomer(string customerName);
    CustomerDto GetCustomerById(int? id);
    void Dispose();
}