using System.ComponentModel.DataAnnotations;

namespace Studly.BLL.DTO.Customer;

public class CustomerPassUpdateDto
{
    [Required]
    [MinLength(8)]
    [MaxLength(20)]
    public string OldPassword { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    [MaxLength(20)]
    public string NewPassword { get; set; } = string.Empty;
}