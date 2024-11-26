using Studly.DAL.Enums;

namespace Studly.BLL.DTO.Challenge;

public class ChallengeDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ChallengePriority Priority { get; set; }
    public ChallengeStatus Status { get; set; }

    public int CustomerId { get; set; }
    public int? ParentChallengeId { get; set; }
    public int TimeTrackingSessionId { get; set; }


    public List<ChallengeDto> SubTasks { get; set; }
}