using System.ComponentModel.DataAnnotations;
using Studly.DAL.Enums;

namespace Studly.BLL.DTO.Challenge;

public class ChallengeRegistrationDto
{
    [Required]
    [StringLength(50, ErrorMessage = "Title limit is 50 chars!")]
    public string Title { get; set; }

    [StringLength(1000, ErrorMessage = "Maximum length is 1000 chars!")]
    public string Description { get; set; }

    public DateTime? Deadline { get; set; }

    [Required] public ChallengePriority Priority { get; set; }

    public ChallengeStatus Status { get; set; } = ChallengeStatus.NotStarted;
}