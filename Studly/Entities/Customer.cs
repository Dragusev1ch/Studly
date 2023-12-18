using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Studly.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(10)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(64)] 
        public string Password { get; set; } = string.Empty;

        //[Required]
        //[MaxLength(64)] 
        //public string Salt { get; set; } = string.Empty;

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public ICollection<Challenge> Tasks { get; set; } = new List<Challenge>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
