using System.ComponentModel.DataAnnotations;

namespace Studly.BLL.DTO.Customer;

public class CustomerListDto
{
    [Key]
    public int Id { get; set; }
    [Required] [MaxLength(10)]
    public string Name { get; set; } = string.Empty;
    [Required] [MaxLength(20)] [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
}