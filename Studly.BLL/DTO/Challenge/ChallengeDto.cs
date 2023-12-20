using Studly.BLL.DTO.SubTask;
using Studly.DAL.Enums;

namespace Studly.BLL.DTO.Challenge;

public class ChallengeDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? DeadLine { get; set; }
    public ChallengePriority Priority { get; set; }
    public ChallengeStatus Status { get; set; }
    public List<SubTaskRegistrationDto> SubTasks { get; set; } 
}