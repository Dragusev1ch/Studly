using System.ComponentModel.DataAnnotations;

namespace Studly.BLL.DTO.Customer;

public class CustomerUpdateDTO
{
    [Required]
    [MinLength(8)]
    [MaxLength(20)]
    public string OldPassword { get; set; }

    [Required]
    [MinLength(8)]
    [MaxLength(20)]
    public string NewPassword { get; set; }
}