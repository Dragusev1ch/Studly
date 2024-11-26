using Studly.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace Studly.BLL.DTO.Challenge;

public class ChallengeUpdateDto
{
    [Required]
    [StringLength(50, ErrorMessage = "Title limit is 50 chars!")]
    public string? Title { get; set; } = string.Empty;
    [StringLength(1000, ErrorMessage = "Maximum length is 1000 chars!")]
    public string? Description { get; set; } = string.Empty;
    public ChallengePriority? Priority { get; set; }
    public ChallengeStatus? Status { get; set; }
    public int TimeTrackingSessionId { get; set; }
}