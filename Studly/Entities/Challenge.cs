﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Studly.Entities
{
    public class Challenge
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime? Deadline { get; set; }

        //public string Priority { get; set; }

        //public string Status { get; set; }

        public int UserId { get; set; }
        public Customer? Customer { get; set; }
        public ICollection<Clock> Clocks { get; set; } = new List<Clock>();
        public ICollection<TaskLabel> TaskLabels { get; set; } = new List<TaskLabel>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }

}
