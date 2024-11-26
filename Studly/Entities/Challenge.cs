using System.ComponentModel.DataAnnotations;
using Studly.DAL.Enums;
using Studly.Entities;

namespace Studly.DAL.Entities
{
    public class Challenge : BaseEntity
    {
        [Required] [MaxLength(20)] 
        public string Title { get; set; } = string.Empty;
        [Required] 
        public string Description { get; set; } = string.Empty;
        [Required]
        public DateTime? Deadline { get; set; }
        public ChallengePriority Priority { get; set; }
        public ChallengeStatus Status { get; set; }

        public int CustomerId { get; set; }
        public int? ParentChallengeId { get; set; }

        public Challenge? ParentChallenge { get; set; }
        public Customer Customer { get; set; }

        public ICollection<Challenge> SubTasks { get; set; } = new List<Challenge>();
    }
}
