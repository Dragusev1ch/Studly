using Studly.BLL.DTO.Customer;

namespace Studly.BLL.Interfaces;

public interface ICustomerService
{
    public void CreateCustomer(CustomerDTO customerDto);
    public CustomerDTO GetCustomer(CustomerLoginDTO  customerLoginDto);
    public IEnumerable<CustomerDTO> List();
    public CustomerDTO Update(CustomerDTO customer);
    public bool Delete(int id);
    CustomerDTO GetCustomerById(int? id);
    void Dispose();
}