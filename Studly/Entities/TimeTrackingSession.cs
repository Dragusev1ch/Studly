using System.ComponentModel.DataAnnotations;

namespace Studly.DAL.Entities;

public class TimeTrackingSession : BaseEntity
{
    [Required]
    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public bool IsPaused { get; set; }

    public int ChallengeId { get; set; }
    public Challenge Challenge { get; set; }
}