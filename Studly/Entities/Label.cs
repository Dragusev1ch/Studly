using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studly.Entities
{
    public class Label
    {
        [Key]
        public int LabelId { get; set; }

        public int UserId { get; set; }
        public Customer? Customer { get; set; }

        public string LabelName { get; set; } = string.Empty;

        public ICollection<TaskLabel> TaskLabels { get; set; } = new List<TaskLabel>();
    }
}
