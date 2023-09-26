using Studly.BLL.DTO.Customer;

namespace Studly.BLL.Interfaces;

public interface ICustomerService
{
    public void CreateCustomer(CustomerRegistrationDTO customerDto);
    public CustomerDTO GetCustomer(CustomerLoginDTO  customerLoginDto);
    public CustomerDTO GetCurrentCustomer(string customerName);
    public IEnumerable<CustomerDTO> List();
    public CustomerDTO Update(CustomerUpdateDTO newCustomer, string email);
    public bool Delete(int id);
    public bool DeleteCurrentCustomer(string customerName);
    CustomerDTO GetCustomerById(int? id);
    void Dispose();
}