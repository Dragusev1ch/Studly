using System.ComponentModel.DataAnnotations;
using Studly.DAL.Enums;

namespace Studly.DAL.Entities
{
    public class Challenge
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? Deadline { get; set; }
        public ChallengePriority Priority { get; set; }
        public ChallengeStatus Status { get; set; }

        public ICollection<SubTask> SubTasks { get; set; } = new List<SubTask>();
    }

}
