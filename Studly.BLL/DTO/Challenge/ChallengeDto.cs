using Studly.DAL.Enums;

namespace Studly.BLL.DTO.Challenge;

public class ChallengeDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? DeadLine { get; set; }
    public ChallengePriority Priority { get; set; }
    public ChallengeStatus Status { get; set; }
    public List<ChallengeRegistrationDto> SubTasks { get; set; } 
}