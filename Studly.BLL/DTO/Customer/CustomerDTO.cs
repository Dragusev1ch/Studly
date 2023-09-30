using System.ComponentModel.DataAnnotations;

namespace Studly.BLL.DTO.Customer;

public class CustomerDTO
{
    public int CustomerId { get; set; }

    [Required] [MaxLength(10)] public string Name { get; set; }

    [Required]
    [MaxLength(20)]
    [EmailAddress]
    public string Email { get; set; }

    public DateTime RegistrationDate { get; set; }
}