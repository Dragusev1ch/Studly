using System.ComponentModel.DataAnnotations;

namespace Studly.BLL.DTO.Customer;

public class CustomerRegistrationDTO
{
    [Required] [MaxLength(10)] public string Name { get; set; }

    [Required]
    [MaxLength(20)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(8)]
    [MaxLength(20)]
    public string Password { get; set; }
}