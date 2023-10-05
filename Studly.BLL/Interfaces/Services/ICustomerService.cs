using Studly.BLL.DTO.Customer;

namespace Studly.BLL.Interfaces.Services;

public interface ICustomerService
{
    Task CreateCustomer(CustomerRegistrationDTO customerDto);
    Task<CustomerDTO> GetCustomer(CustomerLoginDTO  customerLoginDto);
    Task<CustomerDTO> GetCurrentCustomer(string email); 
    Task<IEnumerable<CustomerDTO>> List();
    Task<CustomerDTO> Update(CustomerUpdateDTO newCustomer, string email); 
    Task<int> Delete(int id);
    Task<int> DeleteCurrentCustomer(string customerName);
    Task<CustomerDTO> GetCustomerById(int id);
    void Dispose();
}