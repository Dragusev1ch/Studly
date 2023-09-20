namespace Studly.PL.Models;

public class CustomerConstants
{
    public static List<CustomerViewModel> Customers = new()
    {
        new CustomerViewModel()
        {
            CustomerId = 0, Email = "admin@email.com", Name = "Nazar", Password = "admin",
            RegistrationDate = DateTime.Now, Role = "Administrator"
        }
    };
}
