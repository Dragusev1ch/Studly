using Studly.BLL.DTO.Challenge;
using Studly.BLL.DTO.Customer;

namespace Studly.BLL.Interfaces.Services;

public interface ICustomerService : IDisposable
{
    public void Create(CustomerRegistrationDTO customerDto);
    public CustomerDto? Get(CustomerLoginDTO customerLoginDto);
    public CustomerDto GetCurrent(string email);
    public IQueryable<ChallengeDto>? GetChallenges(string email);
    public CustomerDto Update(CustomerPassUpdateDto newCustomerPass, string email);
    public CustomerDto Delete(string email);
}