using Studly.BLL.DTO;

namespace Studly.BLL.Interfaces;

public interface ICustomerService
{
    void CreateCustomer(CustomerDTO customerDto);
    CustomerDTO GetCustomerById(int? id);
    IEnumerable<CustomerDTO> GetCustomers();
    void Dispose();
}