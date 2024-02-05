using System.ComponentModel.DataAnnotations;
using Studly.BLL.DTO.Challenge;

namespace Studly.BLL.DTO.Customer;

public class CustomerDto
{
    [Key]
    public int Id { get; set; }

    [Required] [MaxLength(10)] 
    public string Name { get; set; }

    [Required] [MaxLength(20)] [EmailAddress]
    public string Email { get; set; }

    public DateTime RegistrationDate { get; set; }

}