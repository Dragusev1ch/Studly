using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studly.Entities
{
    public class Timer
    {
        [Key]
        public int TimerId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int TaskId { get; set; }
        public Task? Task { get; set; }
    }
}
